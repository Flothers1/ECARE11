namespace ECARE.Interface.FileStorage
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
        Task<string> SaveFile(string containerName, IFormFile file);
    }
}
