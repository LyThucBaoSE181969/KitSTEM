using SWP.KitStem.Service.BusinessModels.RequestModel;

namespace SWP.KitStem.Service.Services.IService
{
    public interface IKitService
    {
        Task<ResponseService> GetKitsAsync();
    }
}
