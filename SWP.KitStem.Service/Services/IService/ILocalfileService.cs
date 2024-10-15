using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services.IService
{
    public interface ILocalfileService
    {
        Task<ResponseService> UploadFileAsync(string folder, string fileName, IFormFile file);
        Task<ResponseService> UploadFilesAsync(string folder, Dictionary<string, IFormFile>? nameFiles);
        Task<ResponseService> DownloadFileAsync(string pathToFile);
    }
}

