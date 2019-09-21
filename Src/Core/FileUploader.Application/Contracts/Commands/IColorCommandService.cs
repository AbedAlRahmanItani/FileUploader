using FileUploader.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Commands
{
    public interface IColorCommandService
    {
        Task CreateBulk(IEnumerable<ColorViewModel> colorViewModels);
    }
}
