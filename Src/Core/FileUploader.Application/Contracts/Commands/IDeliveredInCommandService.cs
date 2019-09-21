using FileUploader.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Commands
{
    public interface IDeliveredInCommandService
    {
        Task CreateBulk(IEnumerable<DeliveredInViewModel> deliveredInViewModels);
    }
}