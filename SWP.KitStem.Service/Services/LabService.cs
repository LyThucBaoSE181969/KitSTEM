using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.BusinessModels.ResponseModel;

namespace SWP.KitStem.Service.Services
{
    public class LabService 
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LabService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseService> GetFileUrlByIdAsync(Guid id)
        {
            var serviceResponse = new ResponseService();
            try
            {
                var lab = await _unitOfWork.Labs.GetByIdAsync(id);
                if (lab == null)
                {
                    return serviceResponse
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Retrieving lab information failed!")
                        .AddError("notFound", "Lab not found");
                }

                return serviceResponse
                            .SetSucceeded(true) 
                            .AddDetail("url", lab.Url)
                            .AddDetail("fileName", lab.Name);
            }
            catch
            {
                return serviceResponse
                        .SetSucceeded(false)
                        .AddDetail("message", "Retrieving lab information failed!")
                        .AddError("outOfService", "Could not get current lab information or please check the information again!");
            }
        }
        public async Task<ResponseService> UpdateLabsAsync(LabUpdateRequest request, string? url)
        {
            try
            {
                var lab = await _unitOfWork.Labs.GetByIdAsync(request.Id);
                if (lab == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Chỉnh sửa bài lab thất bại!")
                        .AddError("invalidCredentials", "Không tìm thấy bài lab để chỉnh sửa!");
                }

                lab.LevelId = request.LevelId;
                lab.KitId = request.KitId;
                lab.Name = request.Name!;
                lab.Author = request.Author;
                lab.Price = request.Price;
                if (url != null)
                {
                    lab.Url = url;
                }

                await _unitOfWork.Labs.Update(lab);

                return new ResponseService()
                        .SetSucceeded(true)
                        .AddDetail("message", "Chỉnh sửa bài lab thành công!");
            }
            catch
            {
                return new ResponseService()
                        .SetSucceeded(false)
                        .AddDetail("message", "Chỉnh sửa bài lab thất bại!")
                        .AddError("outOfService", "Không thể tạo mới bài lab ngay lúc này");
            }
        }

        public async Task<ResponseService> DeleteLabsAsync(Guid id)
        {
            try
            {
                var lab = await _unitOfWork.Labs.GetByIdAsync(id);
                if (lab == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Delete failed")
                        .AddError("notFound", "Cannot found");
                }
                _unitOfWork.Labs.Delete(lab);
                await _unitOfWork.SaveAsync();
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Delete completed");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Delete failed!")
                    .AddError("outOfService", "Cannot delete!");
            }
        }

        public async Task<ResponseService> GetLabByIdAsync(Guid id)
        {
            try
            {
                var lab = await _unitOfWork.Labs.GetByIdAsync(id);
                var labDTO = _mapper.Map<LabModelResponse>(lab);
                return new ResponseService()
                            .AddDetail("message", "Lấy thông tin bài lab thành công!")
                            .AddDetail("data", new { lab });
            }
            catch
            {
                return new ResponseService()
                        .SetSucceeded(false)
                        .AddDetail("message", "Lấy thông tin bài lab không thành công!")
                        .AddError("outOfService", "Không thể lấy được thông tin bài lab hiện tại hoặc vui lòng kiểm tra lại thông tin!");
            }
        }

        public async Task<ResponseService> GetLabsAsync()
        {
            try
            {
                var labs = await _unitOfWork.Labs.GetAsync();
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Get labs success")
                    .AddDetail("data", new { labs });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get labs fail")
                    .AddError("error", "Cannot get labs");
            }
        }

        public async Task<ResponseService> CreateLabAsync(LabCreateRequest request, Guid id, string url)
        {
            try
            {
                var lab = _mapper.Map<Lab>(request);
                lab.Id = id;
                lab.Url = url;
                await _unitOfWork.Labs.InsertAsync(lab);

                return new ResponseService()
                        .SetSucceeded(true)
                        .AddDetail("message", "Thêm mới bài lab thành công!");
            }
            catch
            {
                return new ResponseService()
                        .SetSucceeded(false)
                        .AddDetail("message", "Thêm mới bài lab thất bại!")
                        .AddError("outOfService", "Không thể tạo mới bài lab ngay lúc này");
            }
        }
    }
}
