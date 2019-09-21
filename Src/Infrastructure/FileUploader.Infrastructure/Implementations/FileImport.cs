using FileUploader.Application.Contracts.Commands;
using FileUploader.Application.Contracts.Infrastructure;
using FileUploader.Application.ViewModels;
using FileUploader.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Infrastructure.Implementations
{
    public class FileImport: IFileImport
    {
        #region -- Properties --

        private readonly ILogger<FileImport> _logger;
        private readonly IArticleCommandService _articleCommandService;
        private readonly IArticlePriceCommandService _articlePriceCommandService;
        private readonly IColorCommandService _colorCommandService;
        private readonly IDeliveredInCommandService _deliveredInCommandService;
        private readonly ISectionCommandService _sectionCommandService;
        private readonly ISizeCommandService _sizeCommandService;

        #endregion

        #region -- Ctrs --

        public FileImport(ILogger<FileImport> logger,
            IArticleCommandService articleCommandService,
            IArticlePriceCommandService articlePriceCommandService,
            IColorCommandService colorCommandService,
            IDeliveredInCommandService deliveredInCommandService,
            ISectionCommandService sectionCommandService,
            ISizeCommandService sizeCommandService)
        {
            _logger = logger;
            _articleCommandService = articleCommandService;
            _articlePriceCommandService = articlePriceCommandService;
            _colorCommandService = colorCommandService;
            _deliveredInCommandService = deliveredInCommandService;
            _sectionCommandService = sectionCommandService;
            _sizeCommandService = sizeCommandService;
        }

        #endregion

        #region -- Public Methods --

        public async Task ImportAsync(string file)
        {
            // Read the CSV lines into a list of raw objects
            var articlePriceRawModels = ReadCsvLines(file);

            // Convert and Save Referentials that are required to Save Articles Prices
            await SaveReferentialsAsync(articlePriceRawModels);

            // Convert and Save Articles Prices
            await SaveArticlePricesAsync(articlePriceRawModels);

            // Delete Tmp File
            if (File.Exists(file))
                File.Delete(file);
        }

        #endregion

        #region -- Private Methods --

        private static List<ArticlePriceRawModel> ReadCsvLines(string file)
        {
            return File.ReadAllLines(file)
                .Skip(1) // Skip the header line
                .Select(ArticlePriceRawModel.FromCsv)
                .ToList();
        }

        private async Task SaveReferentialsAsync(IList<ArticlePriceRawModel> articlePriceRawModels)
        {
            var articlesTask = CreateArticlesAsync(articlePriceRawModels);
            var colorsTask = CreateColorsAsync(articlePriceRawModels);
            var deliveredInsTask = CreateDeliveredInsAsync(articlePriceRawModels);
            var sectionsTask = CreateSectionsAsync(articlePriceRawModels);
            var sizesTask = CreateSizesAsync(articlePriceRawModels);

            await Task.WhenAll(articlesTask, colorsTask, deliveredInsTask, sectionsTask, sizesTask);
        }

        private async Task CreateArticlesAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var articleViewModels = articlePriceRawModels
                .Select(x => new ArticleViewModel
                {
                    Code = x.ArticleCode,
                    Label = x.ArticleLabel,
                    Description = x.Description
                })
                .GroupBy(x => x.Code)
                .Select(x => x.First());

            await _articleCommandService.CreateBulk(articleViewModels);
        }

        private async Task CreateColorsAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var colorViewModels = articlePriceRawModels
                .Select(x => new ColorViewModel
                {
                    Code = x.Color
                })
                .GroupBy(x => x.Code)
                .Select(x => x.First());

            await _colorCommandService.CreateBulk(colorViewModels);
        }

        private async Task CreateDeliveredInsAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var deliveredInViewModels = articlePriceRawModels
                .Select(x => new DeliveredInViewModel
                {
                    Code = x.DeliveredIn
                })
                .GroupBy(x => x.Code)
                .Select(x => x.First());

            await _deliveredInCommandService.CreateBulk(deliveredInViewModels);
        }

        private async Task CreateSectionsAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var sectionViewModels = articlePriceRawModels
                .Select(x => new SectionViewModel
                {
                    Code = x.Q1
                })
                .GroupBy(x => x.Code)
                .Select(x => x.First());

            await _sectionCommandService.CreateBulk(sectionViewModels);
        }

        private async Task CreateSizesAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var sizeViewModels = articlePriceRawModels
                .Select(x => new SizeViewModel
                {
                    Code = x.Size
                })
                .GroupBy(x => x.Code)
                .Select(x => x.First());

            await _sizeCommandService.CreateBulk(sizeViewModels);
        }

        private async Task SaveArticlePricesAsync(IEnumerable<ArticlePriceRawModel> articlePriceRawModels)
        {
            var articlePriceViewModels = articlePriceRawModels
                .Select(x => new ArticlePriceViewModel
                {
                    Key = x.Key,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    ArticleCode = x.ArticleCode,
                    ColorCode = x.Color,
                    DeliveredInCode = x.DeliveredIn,
                    SectionCode = x.Q1,
                    SizeCode = x.Size
                });

            await _articlePriceCommandService.CreateBulk(articlePriceViewModels);
        }

        #endregion
    }
}