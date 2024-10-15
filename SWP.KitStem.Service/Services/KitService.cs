using AutoMapper;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.Services.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.BusinessModels.ResponseModel;

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

        //public async Task<ResponseService> GetKitsAsync()
        //{
        //    try
        //    {
        //        var kits = await _unitOfWork.Kits.GetAsync();
        //        return new ResponseService()
        //            .SetSucceeded(true)
        //            .AddDetail("message", "Get kits success")
        //            .AddDetail("data", new { kits });
        //        if (kits.Count() > 0)
        //        {

        //        }

        //    }
        //    catch
        //    {
        //        return new ResponseService()
        //            .SetSucceeded(false)
        //            .AddDetail("message", "Get kits fail")
        //            .AddError("error", "Cannot get kits");
        //    }
        //}

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

        private Expression<Func<Kit, bool>> GetFilter(KitModelRequest kitModel)
        {
            return (l) => l.Name.ToLower().Contains(kitModel.KitName.ToLower()) && l.Category.Name.ToLower().Contains(kitModel.CategoryName.ToLower());
        }
    }                                    
}
