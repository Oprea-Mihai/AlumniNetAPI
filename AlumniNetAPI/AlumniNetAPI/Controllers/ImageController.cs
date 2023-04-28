using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly string _accessKey = "YOUR_AWS_ACCESS_KEY";
        private readonly string _secretKey = "YOUR_AWS_SECRET_KEY";
        private readonly string _bucketName = "YOUR_AWS_BUCKET_NAME";
        private readonly RegionEndpoint _region = RegionEndpoint.EUCentral1;

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            try
            {
                // create an Amazon S3 client instance
                var s3Client = new AmazonS3Client(_accessKey, _secretKey, _region);

                // generate a unique file name for the image
                var fileName = Guid.NewGuid().ToString();

                // create a TransferUtility instance to upload the image
                var transferUtility = new TransferUtility(s3Client);

                // upload the image to S3
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    await transferUtility.UploadAsync(stream, _bucketName, fileName);
                }

                // save the S3 object key or URL in your database for future retrieval
                var s3ObjectKey = $"{fileName}";

                return Ok(s3ObjectKey);
            }
            catch (AmazonS3Exception ex)
            {
                return BadRequest($"Error uploading file: {ex.Message}");
            }
        }
    }
}
