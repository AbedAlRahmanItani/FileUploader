using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FileUploader.Application.Contracts.Infrastructure;
using FileUploader.WebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace FileUploader.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        private readonly ILogger<StreamingController> _logger;
        private readonly IFileImport _fileImport;

        // Get the default form options so that we can use them to set the default limits for request body data
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        public StreamingController(ILogger<StreamingController> logger, IFileImport fileImport)
        {
            _logger = logger;
            _fileImport = fileImport;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                {
                    return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
                }

                // Used to accumulate all the form url encoded key value pairs in the request.
                var formAccumulator = new KeyValueAccumulator();
                string targetFilePath = null;

                var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, HttpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            targetFilePath = Path.GetTempFileName();
                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);

                                _logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                            }
                        }
                        else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        {
                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = GetEncoding(section);
                            using (var streamReader = new StreamReader(section.Body, encoding, true, 1024, true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                                if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = string.Empty;
                                }
                                formAccumulator.Append(key.ToString(), value);

                                if (formAccumulator.ValueCount > DefaultFormOptions.ValueCountLimit)
                                {
                                    throw new InvalidDataException($"Form key count limit {DefaultFormOptions.ValueCountLimit} exceeded.");
                                }
                            }
                        }
                    }

                    // Drains any remaining section body that has not been consumed and reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }

                if (string.IsNullOrEmpty(targetFilePath) || !System.IO.File.Exists(targetFilePath))
                {
                    _logger.LogError("File not created!");
                    return StatusCode(500, "Internal server error");
                }

                // Import the file
                Stopwatch sw = new Stopwatch();
                sw.Start();
                await _fileImport.ImportAsync(targetFilePath);
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                _logger.LogInformation(string.Format("Import run time: {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}