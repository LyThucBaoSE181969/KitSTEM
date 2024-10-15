using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services.IService
{
    public interface IKitImageService
    {
        Task<ResponseService> GetImageAsync(int id);
        Task<ResponseService> CreateImageAsync(Guid id, int kitId, String url);
        Task<ResponseService> RemoveImageAsync(int kitId);
        Task<ResponseService> UpdateImageAsync(Guid id, int kitId, string url);
    }
}
