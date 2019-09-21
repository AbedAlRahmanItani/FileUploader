using FileUploader.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Commands
{
    public interface IArticlePriceCommandService
    {
        Task CreateBulk(IEnumerable<ArticlePriceViewModel> articlePriceViewModels);
    }
}
