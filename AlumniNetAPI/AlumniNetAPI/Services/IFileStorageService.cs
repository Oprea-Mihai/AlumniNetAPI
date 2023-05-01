using System.Threading.Tasks;

namespace AlumniNetAPI.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string prefix = "", string bucketName = "alumni-app-bucket" );
        Task DeleteFileByKeyAsync(string key, string bucketName = "alumni-app-bucket");
        Task<Stream> GetFileByKeyAsync(string key, string bucketName = "alumni-app-bucket");
    }
}
