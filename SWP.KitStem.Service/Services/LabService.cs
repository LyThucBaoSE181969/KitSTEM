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
    public class LabService
    {
        private readonly UnitOfWork _unitOfWork;

        public LabService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LabModel>> GetLabsAsync()
        {
            var labs = await _unitOfWork.Labs.GetAsync();
            return labs.Select(labs => new LabModel
            {
                Id = labs.Id,
                LevelId = labs.LevelId,
                KitId = labs.KitId,
                Name = labs.Name,
                Url = labs.Url,
                Price = labs.Price,
                MaxSupportTimes = labs.MaxSupportTimes,
                Author = labs.Author,
                Status = labs.Status,
                Kit = labs.Kit,
                Level = labs.Level,
                //OrderSupports = labs.OrderSupports,
                //Packages = labs.Packages
            });
        }

        public async Task<LabModel> GetLabByIdAsync(Guid id)
        {
            var lab = await _unitOfWork.Labs.GetByIdAsync(id);
            if (lab == null) return null;

            return new LabModel
            {
                Id = lab.Id,
                LevelId = lab.LevelId,
                KitId = lab.KitId,
                Name = lab.Name,
                Url = lab.Url,
                Price = lab.Price,
                MaxSupportTimes = lab.MaxSupportTimes,
                Author = lab.Author,
                Status = lab.Status,
                Kit = lab.Kit,
                Level = lab.Level,
                //OrderSupports = lab.OrderSupports,
                //Packages = lab.Packages
            };
        }

        public async Task<bool> UpdateLabAsync(Guid id, LabModel labModel)
        {
            var labToUpdate = await _unitOfWork.Labs.GetByIdAsync(id);
            if (labToUpdate == null) return false;

            labToUpdate.LevelId = labModel.LevelId;
            labToUpdate.KitId = labModel.KitId;
            labToUpdate.Name = labModel.Name;
            labToUpdate.Url = labModel.Url;
            labToUpdate.Price = labModel.Price;
            labToUpdate.MaxSupportTimes = labModel.MaxSupportTimes;
            labToUpdate.Author = labModel.Author;
            labToUpdate.Status = labModel.Status;
            labToUpdate.Kit = labModel.Kit;
            labToUpdate.Level = labModel.Level;
            labToUpdate.OrderSupports = labModel.OrderSupports;
            labToUpdate.Packages = labModel.Packages;

            _unitOfWork.Labs.Update(labToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Guid> InsertLabAsync(LabModel labModel)
        {
            var labEntity = new Lab
            {
                LevelId = labModel.LevelId,
                KitId = labModel.KitId,
                Name = labModel.Name,
                Url = labModel.Url,
                Price = labModel.Price,
                MaxSupportTimes = labModel.MaxSupportTimes,
                Author = labModel.Author,
                Status = labModel.Status,
                Kit = labModel.Kit,
                Level = labModel.Level,
                OrderSupports = labModel.OrderSupports,
                Packages = labModel.Packages
            };

            await _unitOfWork.Labs.InsertAsync(labEntity);
            await _unitOfWork.SaveAsync();
            return labEntity.Id;
        }

        public async Task<bool> DeleteLabAsync(Guid id)
        {
            var lab = await _unitOfWork.Labs.GetByIdAsync(id);
            if (lab == null) return false;

            _unitOfWork.Labs.Delete(lab);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> LabExistAsync(Guid id)
        {
            return await _unitOfWork.Labs.IsExist(id);
        }

    }
}
