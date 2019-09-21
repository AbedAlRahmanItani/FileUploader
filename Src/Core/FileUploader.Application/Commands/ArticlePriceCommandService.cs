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
    public class ArticlePriceCommandService : IArticlePriceCommandService
    {
        private readonly IApplicationDbContext _context;

        public ArticlePriceCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<ArticlePriceViewModel> articlePriceViewModels)
        {
            var articlePrices = articlePriceViewModels
                .Select(x => new ArticlePrice
                {
                    Key = x.Key,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    ArticleCode = x.ArticleCode,
                    ColorCode = x.ColorCode,
                    DeliveredInCode = x.DeliveredInCode,
                    SectionCode = x.SectionCode,
                    SizeCode = x.SizeCode
                })
                .ToList();

            foreach (var articlePrice in articlePrices)
            {
                if (_context.ArticlePrices.Any(x => articlePrice.Key.Equals(x.Key, StringComparison.InvariantCultureIgnoreCase)))
                {
                    // TODO: Log message that the key is already in DB
                    continue;
                }

                await _context.ArticlePrices.AddAsync(articlePrice);
            }

            await _context.SaveChangesAsync();
        }
    }
}
