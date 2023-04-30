using AlumniNetAPI.DTOs;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AlumniNetAPI.Controllers
{   
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IAmazonS3 _s3Client;
        public FilesController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        [Authorize]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName="alumni-app-bucket", string prefix="")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            string key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}";
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = key,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return Ok(key);
        }

        [Authorize]
        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFilesAsync(string bucketName = "alumni-app-bucket", string prefix = "")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };
            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ObjectDto()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });
            return Ok(s3Objects);
        }

        [Authorize]
        [HttpGet("GetFileByKey")]
        public async Task<IActionResult> GetFileByKeyAsync(string key, string bucketName= "alumni-app-bucket")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
            return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        }

        [Authorize]
        [HttpDelete("DeleteFileByKey")]
        public async Task<IActionResult> DeleteFileByKeyAsync(string key,string bucketName= "alumni-app-bucket")
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist");
            await _s3Client.DeleteObjectAsync(bucketName, key);
            return Ok("Successfully deleted!");
        }
    }
}
