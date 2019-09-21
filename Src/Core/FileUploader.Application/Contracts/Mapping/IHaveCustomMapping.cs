using AutoMapper;

namespace FileUploader.Application.Contracts.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}