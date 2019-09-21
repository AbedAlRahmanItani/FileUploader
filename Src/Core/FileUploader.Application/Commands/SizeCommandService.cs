using FileUploader.Application.Contracts.Commands;
using FileUploader.Application.Contracts.Context;
using FileUploader.Application.ViewModels;
using FileUploader.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Application.Commands
{
    public class SizeCommandService : ISizeCommandService
    {
        private readonly IApplicationDbContext _context;

        public SizeCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<SizeViewModel> sizeViewModels)
        {
            var sizes = sizeViewModels
                 .Select(x => new Size
                 {
                     Code = x.Code
                 })
                .ToList();

            foreach (var size in sizes)
            {
                if (!_context.Sizes.Any(x => size.Code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await _context.Sizes.AddAsync(size);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
