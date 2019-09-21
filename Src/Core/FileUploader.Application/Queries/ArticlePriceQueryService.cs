using AutoMapper;
using FileUploader.Application.Contracts.Context;
using FileUploader.Application.Contracts.Queries;
using FileUploader.Application.Exceptions;
using FileUploader.Application.ViewModels;
using FileUploader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileUploader.Application.Queries
{
    public class ArticlePriceQueryService: IArticlePriceQueryService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArticlePriceQueryService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArticlePriceViewModel> GetByKey(string key)
        {
            var articlePrice = _mapper.Map<ArticlePriceViewModel>(await _context.ArticlePrices.SingleOrDefaultAsync(p => p.Key.Equals(key)));

            if (articlePrice == null)
                throw new NotFoundException(nameof(ArticlePrice), key);
            
            return articlePrice;
        }
    }
}
