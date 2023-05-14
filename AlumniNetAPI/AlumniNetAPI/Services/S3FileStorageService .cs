using Amazon.S3.Model;
using Amazon.S3;
using AlumniNetAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Services
{
    public class S3FileStorageService:IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;

        public S3FileStorageService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string prefix = "", string bucketName = "alumni-app-bucket")
        {
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) throw new Exception($"Bucket {bucketName} does not exist.");
                string key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{file.FileName}-{prefix?.TrimEnd('/')}";
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream()
                };
                request.Metadata.Add("Content-Type", file.ContentType);
                await _s3Client.PutObjectAsync(request);
                return key;
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
                return string.Empty;
            }
           
        }

        public async Task DeleteFileByKeyAsync(string key,string bucketName= "alumni-app-bucket")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) throw new Exception($"Bucket {bucketName} does not exist.");

            await _s3Client.DeleteObjectAsync(bucketName, key);
        }

        public async Task<Stream> GetFileByKeyAsync(string key, string bucketName = "alumni-app-bucket")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)  throw new Exception($"Bucket {bucketName} does not exist.");
            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
           return s3Object.ResponseStream;
        }
    }
}

