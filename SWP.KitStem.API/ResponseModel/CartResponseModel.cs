using SWP.KitStem.Repository.Models;

namespace SWP.KitStem.API.ResponseModel
{
    public class CartResponseModel
    {
        public string UserId { get; set; } = null!;

        public int PackageId { get; set; }

        public int PackageQuantity { get; set; }

        public virtual Package Package { get; set; } = null!;

        public virtual AspNetUser User { get; set; } = null!;
    }
}
