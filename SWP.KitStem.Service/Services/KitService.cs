using SWP.KitStem.Repository.Models;
using SWP.KitStem.Repository;
using SWP.KitStem.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class KitService
    {
        private readonly UnitOfWork _unitOfWork;

        public KitService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KitModel>> GetKitsAsync()
        {
            var kits = await _unitOfWork.Kits.GetAsync();
            return kits.Select(kit => new KitModel
            {
                Id = kit.Id,
                CategoryId = kit.CategoryId,
                Name = kit.Name,
                Brief = kit.Brief,
                Description = kit.Description,
                PurchaseCost = kit.PurchaseCost,
                Status = kit.Status,
            });
        }

        public async Task<KitModel> GetKitByIdAsync(int id)
        {
            var kit = await _unitOfWork.Kits.GetByIdAsync(id);
            if (kit == null) return null;

            return new KitModel
            {
                Id = kit.Id,
                CategoryId = kit.CategoryId,
                Name = kit.Name,
                Brief = kit.Brief,
                Description = kit.Description,
                PurchaseCost = kit.PurchaseCost,
                Status = kit.Status,
                //Images = kit.Images,
                //KitComponents = kit.KitComponents,
                //Labs = kit.Labs,
                //Packages = kit.Packages
            };
        }

        public async Task<bool> UpdateKitAsync(int id, KitModel kitModel)
        {
            var kitToUpdate = await _unitOfWork.Kits.GetByIdAsync(id);
            if (kitToUpdate == null) return false;


            kitToUpdate.CategoryId = kitModel.CategoryId;
            kitToUpdate.Name = kitModel.Name;
            kitToUpdate.Brief = kitModel.Brief;
            kitToUpdate.Description = kitModel.Description;
            kitToUpdate.PurchaseCost = kitModel.PurchaseCost;
            kitToUpdate.Status = kitModel.Status;
            kitToUpdate.Images = kitModel.Images;
            kitToUpdate.KitComponents = kitModel.KitComponents;
            kitToUpdate.Labs = kitModel.Labs;
            kitToUpdate.Packages = kitModel.Packages;

            _unitOfWork.Kits.Update(kitToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<int> InsertKitAsync(KitModel kitModel)
        {
            var kitEntity = new Kit
            {
                CategoryId = kitModel.CategoryId,
                Name = kitModel.Name,
                Brief = kitModel.Brief,
                Description = kitModel.Description,
                PurchaseCost = kitModel.PurchaseCost,
                Status = kitModel.Status,
                Images = kitModel.Images,
                KitComponents = kitModel.KitComponents,
                Labs = kitModel.Labs,
                Packages = kitModel.Packages
            };

            await _unitOfWork.Kits.InsertAsync(kitEntity);
            await _unitOfWork.SaveAsync();
            return kitEntity.Id;
        }

        public async Task<bool> DeleteKitAsync(int id)
        {
            var kit = await _unitOfWork.Kits.GetByIdAsync(id);
            if (kit == null) return false;

            _unitOfWork.Kits.Delete(kit);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> KitExistsAsync(int id)
        {
            return await _unitOfWork.Kits.IsExist(id);
        }
    }
}
