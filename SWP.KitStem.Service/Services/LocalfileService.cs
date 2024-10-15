using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SWP.KitStem.Service.Services.IService;
using SWP.KitStem.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class LocalfileService : ILocalfileService
    {
        private readonly string _localFolderPath;
        public LocalfileService(IOptions<LocalfileSettings> settings)
        {
            _localFolderPath = settings.Value.LocalFolderPath;
        }

        public async Task<ResponseService> UploadFileAsync(string folder, string fileName, IFormFile file)
        {
            var serviceResponse = new ResponseService();
            try
            {
                var filePath = Path.Combine(_localFolderPath, folder, fileName);

                // Xóa file cũ nếu có
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Lưu file vào đường dẫn cục bộ
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return serviceResponse
                        .SetSucceeded(true)
                        .AddDetail("url", filePath); // Trả về đường dẫn của file cục bộ
            }
            catch
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tạo mới file thất bại")
                        .AddError("outOfService", $"Không thể tạo {file.Name} ngay bây giờ!");
            }
        }

        public async Task<ResponseService> UploadFilesAsync(string folder, Dictionary<string, IFormFile>? nameFiles)
        {
            var serviceResponse = new ResponseService();
            try
            {
                var filePaths = new List<string>();

                foreach (var entry in nameFiles)
                {
                    var fileName = entry.Key;
                    var file = entry.Value;
                    var filePath = Path.Combine(_localFolderPath, folder, fileName);

                    // Tạo thư mục nếu chưa tồn tại
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // Lưu file vào đường dẫn cục bộ
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    filePaths.Add(filePath);
                }

                return serviceResponse
                        .SetSucceeded(true)
                        .AddDetail("urls", filePaths);
            }
            catch
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tạo mới files thất bại")
                        .AddError("outOfService", "Không thể tạo files ngay bây giờ!");
            }
        }

        public async Task<ResponseService> DownloadFileAsync(string pathToFile)
        {
            var serviceResponse = new ResponseService();
            try
            {
                var stream = new MemoryStream();
                using (var fileStream = new FileStream(pathToFile, FileMode.Open))
                {
                    await fileStream.CopyToAsync(stream);
                }
                stream.Position = 0;

                return serviceResponse
                        .SetSucceeded(true)
                        .AddDetail("stream", stream)
                        .AddDetail("contentType", "application/octet-stream");
            }
            catch
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tải file thất bại")
                        .AddError("outOfService", $"Không thể tải file ngay bây giờ!");
            }
        }
    }
}
