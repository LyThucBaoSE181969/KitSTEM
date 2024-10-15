using SWP.KitStem.Service.BusinessModels.RequestModel;

namespace SWP.KitStem.Service.Services.IService
{
    public interface IKitService
    {
        //Task<ResponseService> GetKitsAsync(KitModelRequest request);
        Task<ResponseService> GetKitsAsync();
        Task<ResponseService> GetKitByIdAsync(int id);
        Task<ResponseService> CreateKitAsync(KitCreateRequest request);
        Task<ResponseService> UpdateKitAsync(KitUpdateRequest request);
        Task<ResponseService> DeleteKitAsync(int id);
        Task<int> GetMaxIdAsync();

    }
}
