using Microsoft.AspNetCore.Http;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class LevelService
    {
        private readonly UnitOfWork _unitOfWork;

        public LevelService(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<ResponseService> GetLevelByIdAsync(int id)
        {
            try
            {
                var level = await _unitOfWork.Levels.GetByIdAsync(id);
                if (level == null)
                {
                    return new ResponseService()
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status404NotFound)
                    .AddDetail("message", "Get level failed")
                    .AddError("error", "Cannot found levels");
                }
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("data", new { level });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get level failed")
                    .AddError("error", "Cannot get levels");
            }
        }
        public async Task<ResponseService> GetLevelsAsync()
        {
            try
            {
                var levels = await _unitOfWork.Levels.GetAsync();
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Get levels successed")
                    .AddDetail("data", new {levels});
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get levels failed")
                    .AddError("error", "Cannot get levels");

            }
        }   
    }
}
