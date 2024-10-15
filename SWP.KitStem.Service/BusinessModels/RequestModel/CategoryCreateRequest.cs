using System.ComponentModel.DataAnnotations;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class CategoryCreateRequest
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
