using AutoMapper;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.Services.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.BusinessModels.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace SWP.KitStem.Service.Services
{
    public class KitService : IKitService
    {
        private readonly int sizePerPage = 20;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public KitService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> GetMaxIdAsync()
        {
            try
            {
                var kitId = await _unitOfWork.Kits.GetMaxIdAsync();
                return kitId;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<ResponseService> DeleteKitAsync(int id)
        {
            try
            {
                var kit = await _unitOfWork.Kits.GetByIdAsync(id);
                if (kit == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Delete fail!")
                        .AddError("notFound", "Cannot found kit!");
                }
                        
                _unitOfWork.Categories.Delete(kit);
                await _unitOfWork.SaveAsync();
                return new ResponseService()
                            .SetSucceeded(true)
                            .AddDetail("message", "Delete complete!");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Delete fail!")
                    .AddError("outOfService", "Cannot delete!");
            }
        }

        public async Task<ResponseService> UpdateKitAsync(KitUpdateRequest request)
        {
            try
            {
                var kit = await _unitOfWork.Kits.GetByIdAsync(request.Id);
                if (kit == null || !kit.Status)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .AddDetail("message", "Không thể cập nhật kit")
                        .AddError("notFound", "Không tìm thấy kit");
                }

                kit.CategoryId = request.CategoryId;
                kit.Name = request.Name;
                kit.Brief = request.Brief;
                kit.Description = request.Description;
                kit.PurchaseCost = request.PurchaseCost;
                kit.Status = true;
                await _unitOfWork.Kits.Update(kit);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Cập nhật kit thành công");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Cập nhật kit thất bại")
                    .AddError("outOfService", "Không thể cập nhật kit ngay lúc này!");
            }
        }

        public async Task<ResponseService> CreateKitAsync(KitCreateRequest request)
        {
            try
            {
                var kit = _mapper.Map<Kit>(request);
                var kitId = await _unitOfWork.Kits.InsertAsync(kit);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Tạo mới kit thành công!");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Tạo mới kit thất bại!")
                    .AddError("outOfService", "Không thể tạo mới kit ngay lúc này!");
            }
        }

        public async Task<ResponseService> GetKitByIdAsync(int id)
        {
            try
            {
                var kit = await _unitOfWork.Kits.GetByIdAsync(id);
                if (kit == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Get kit fail")
                        .AddError("error", "Cannot found");
                }
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("data", new { kit });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get kit fail")
                    .AddError("error", "Cannot get kit");
            }
        }

        public async Task<ResponseService> GetKitsAsync()
        {
            try
            {
                var kits = await _unitOfWork.Kits.GetAsync();
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Get kits success")
                    .AddDetail("data", new { kits });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get kits fail")
                    .AddError("error", "Cannot get kits");
            }
        }
        //public async Task<ResponseService> GetKitsAsync(KitModelRequest request)
        //{
        //    try
        //    {
        //        var filter = GetFilter(request);

        //        var (Kits, totalPages) = await _unitOfWork.Kits.GetFilterAsync(
        //            filter,
        //            null,
        //            skip: sizePerPage * request.Page,
        //            take: sizePerPage,
        //            query => query.Include(l => l.Category).Include(l => l.KitImages)
        //            );
        //        if (Kits.Count() > 0)
        //        {
        //            var kitsDTO = _mapper.Map<IEnumerable<KitModelRequest>>(Kits);
        //            return new ResponseService()
        //                 .SetSucceeded(true)
        //                 .AddDetail("message", "Lấy danh sách kit thành công")
        //                 .AddDetail("data", new { totalPages, currentPage = (request.Page + 1), kits = kitsDTO });
        //        }
        //        else
        //        {
        //            return new ResponseService()
        //               .SetSucceeded(true)
        //               .AddDetail("message", "Không tìm thấy bộ kit!!!!!");
        //        }
        //    }
        //    catch
        //    {
        //        return new ResponseService()
        //            .SetSucceeded(false)
        //            .AddDetail("message", "Lấy danh sách kit không thành công")
        //            .AddError("outOfService", "Không thể lấy danh sách kit ngay lúc này!");
        //    }
        //}

        //private Expression<Func<Kit, bool>> GetFilter(KitModelRequest kitModel)
        //{
        //    return (l) => l.Name.ToLower().Contains(kitModel.KitName.ToLower()) && l.Category.Name.ToLower().Contains(kitModel.CategoryName.ToLower());
        //}



        

        
    }                                    
}
