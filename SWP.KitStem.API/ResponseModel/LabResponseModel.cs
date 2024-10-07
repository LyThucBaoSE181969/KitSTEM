using SWP.KitStem.Repository.Models;

namespace SWP.KitStem.API.ResponseModel
{
    public class LabResponseModel
    {
        public Guid Id { get; set; }

        public int LevelId { get; set; }

        public int KitId { get; set; }

        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public int Price { get; set; }

        public int MaxSupportTimes { get; set; }

        public string? Author { get; set; }

        public bool Status { get; set; }

        public virtual Kit Kit { get; set; } = null!;

        public virtual Level Level { get; set; } = null!;

        public List<OrderSupport> OrderSupports { get; set; } = new List<OrderSupport>();

        public List<Package> Packages { get; set; } = new List<Package>();
    }
}
