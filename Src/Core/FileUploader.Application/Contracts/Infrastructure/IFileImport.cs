using System.Threading.Tasks;

namespace FileUploader.Application.Contracts.Infrastructure
{
    public interface IFileImport
    {
        Task ImportAsync(string file);
    }
}
