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
    public class ArticleCommandService: IArticleCommandService
    {
        private readonly IApplicationDbContext _context;

        public ArticleCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<ArticleViewModel> articleViewModels)
        {
            var articles = articleViewModels
                .Select(x => new Article
                {
                    Code = x.Code,
                    Label = x.Label,
                    Description = x.Description
                })
                .ToList();

            foreach (var article in articles)
            {
                if (!_context.Articles.Any(x => article.Code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await _context.Articles.AddAsync(article);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
