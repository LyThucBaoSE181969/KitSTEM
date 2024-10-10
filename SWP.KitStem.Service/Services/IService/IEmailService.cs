
using SWP.KitStem.Service.BusinessModels;

namespace SWP.KitStem.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
