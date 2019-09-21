using FileUploader.Application.ViewModels;
using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Queries
{
    public interface IArticlePriceQueryService
    {
        Task<ArticlePriceViewModel> GetByKey(string key);
    }
}
