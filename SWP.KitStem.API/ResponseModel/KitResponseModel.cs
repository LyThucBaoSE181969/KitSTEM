using SWP.KitStem.Repository.Models;

namespace SWP.KitStem.API.ResponseModel
{
    public class KitResponseModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Brief { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int PurchaseCost { get; set; }

        public bool Status { get; set; }

        public virtual Category Category { get; set; } = null!;

        public List<Image> Images { get; set; } = new List<Image>();

        public List<KitComponent> KitComponents { get; set; } = new List<KitComponent>();

        public List<Lab> Labs { get; set; } = new List<Lab>();

        public List<Package> Packages { get; set; } = new List<Package>();

    }
}
