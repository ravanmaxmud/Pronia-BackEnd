using PrioniaApp.Contracts.File;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInFileSystem = GenerateUniqueFileName(formFile.FileName);

            var filePath = Path.Combine(directoryPath, imageNameInFileSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath,FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch(Exception e)
            {
                _logger.LogError(e,"Some Things Went Wrong");
                throw;
            }

            return imageNameInFileSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory),fileName!);

            await Task.Run(() => File.Delete(deletePath));
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.Products:
                    return $"{initialSegment}/products/{fileName}";
                case UploadDirectory.Slider:
                    return $"{initialSegment}/sliders/{fileName}";
                case UploadDirectory.PaymentBenefits:
                    return $"{initialSegment}/paymentBenefits/{fileName}";
                case UploadDirectory.Feedback:
                    return $"{initialSegment}/feedback/{fileName}";
                case UploadDirectory.Reward:
                    return $"{initialSegment}/reward/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Products:
                    return Path.Combine(startPath, "products");
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "sliders");
                case UploadDirectory.PaymentBenefits:
                    return Path.Combine(startPath, "paymentBenefits");
                case UploadDirectory.Feedback:
                    return Path.Combine(startPath, "feedback");
                case UploadDirectory.Reward:
                    return Path.Combine(startPath, "reward");
                default:
                    throw new Exception("Something went wrong");
            }
        }

    }
}
