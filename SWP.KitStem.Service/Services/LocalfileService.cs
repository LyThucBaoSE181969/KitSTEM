using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SWP.KitStem.Service.Services.IService;
using SWP.KitStem.Service.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // Hàm xác định loại nội dung dựa trên phần mở rộng file
        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" }
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        // Hàm xóa file dựa trên tiền tố (prefix) giống FirebaseService
        public async Task DeleteFileWithPrefix(string folder, string prefix)
        {
            var directoryInfo = new DirectoryInfo(Path.Combine(_localFolderPath, folder));
            foreach (var file in directoryInfo.GetFiles(prefix + "*"))
            {
                file.Delete();
            }
        }

        public async Task<ResponseService> UploadFileAsync(string folder, string fileName, IFormFile file)
        {
            var serviceResponse = new ResponseService();
            try
            {
                var filePrefix = $"{folder}/{fileName}";

                var ext = Path.GetExtension(file.FileName);
                var fullFileName = $"{filePrefix}{ext}";

                await DeleteFileWithPrefix(folder, fileName);

                var filePath = Path.Combine(_localFolderPath, fullFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return serviceResponse
               .SetSucceeded(true)
               .AddDetail("url", filePath);  // Đường dẫn cục bộ của file


                
            }
            catch (Exception ex)
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tạo mới file thất bại")
                        .AddError("outOfService", $"Không thể tạo {file.FileName} ngay bây giờ. Chi tiết: {ex.Message}");
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

                    // Xóa file cũ có cùng tiền tố
                    DeleteFileWithPrefix(folder, fileName);

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
            catch (Exception ex)
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tạo mới files thất bại")
                        .AddError("outOfService", $"Không thể tạo files ngay bây giờ. Chi tiết: {ex.Message}");
            }
        }

        public async Task<ResponseService> DownloadFileAsync(string pathToFile)
        {
            var serviceResponse = new ResponseService();
            try
            {
                if (!File.Exists(pathToFile))
                {
                    return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "File không tồn tại")
                        .AddError("fileNotFound", $"File tại {pathToFile} không được tìm thấy.");
                }

                var stream = new MemoryStream();
                using (var fileStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
                {
                    await fileStream.CopyToAsync(stream);
                }
                stream.Position = 0;

                // Xác định loại nội dung dựa trên phần mở rộng file
                var contentType = GetContentType(pathToFile);

                return serviceResponse
                        .SetSucceeded(true)
                        .AddDetail("stream", stream)
                        .AddDetail("contentType", contentType);
            }
            catch (Exception ex)
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Tải file thất bại")
                        .AddError("outOfService", $"Không thể tải file ngay bây giờ. Chi tiết: {ex.Message}");
            }
        }
    }
}
