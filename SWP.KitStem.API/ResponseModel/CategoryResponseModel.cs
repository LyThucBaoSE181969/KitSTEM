using SWP.KitStem.Service.BusinessModels;

namespace SWP.KitStem.API.ResponseModel
{
    public class CategoryResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public List<KitModel> Kits { get; set; } = new List<KitModel>();
    }
}
