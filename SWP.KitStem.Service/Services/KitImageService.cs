using AutoMapper;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using System.Linq.Expressions;

namespace SWP.KitStem.Service.Services
{
    public class KitImageService 
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public KitImageService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<ResponseService> GetImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseService> CreateImageAsync(Guid id, int kitId, String url)
        {
            try
            {
                var kitImage = new KitImage();
                kitImage.KitId = kitId;
                kitImage.Id = id;
                kitImage.Url = url;

                await _unitOfWork.KitImage.InsertAsync(kitImage);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Thêm ảnh thành công");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddError("outOfService", "Không thể tạo kit image ngay lúc này!")
                    .AddDetail("message", "Tạo ảnh thất bại");
            }
        }

        public async Task<ResponseService> RemoveImageAsync(int id)
        {
            try
            {
                Expression<Func<KitImage, bool>> filter = (l) => l.KitId == id;
                var (images, totalPages) = await _unitOfWork.KitImage.GetFilterAsync(filter, null, null, null, null);
                foreach (var image in images)
                {
                    if (!await _unitOfWork.KitImage.RemoveAsync(image))
                        return new ResponseService()
                            .SetSucceeded(false)
                            .AddError("outOfService", "Không thể tạo kit image ngay lúc này!")
                            .AddDetail("message", "Xóa KitImage thất bại");
                }
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "xóa kitimage thành công");
            }
            catch
            {
                return new ResponseService()
                            .SetSucceeded(false)
                            .AddError("outOfService", "Không thể tạo kit image ngay lúc này!")
                            .AddDetail("message", "Xóa KitImage thất bại");
            }
        }

        public async Task<ResponseService> UpdateImageAsync(Guid id, int kitId, string url)
        {
            try
            {
                var kitImage = new KitImage();
                kitImage.KitId = kitId;
                kitImage.Id = id;
                kitImage.Url = url;

                await _unitOfWork.KitImage.Update(kitImage);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Thêm ảnh thành công");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddError("outOfService", "Không thể tạo kit image ngay lúc này!")
                    .AddDetail("message", "Tạo ảnh thất bại");
            }
        }
    }
}
