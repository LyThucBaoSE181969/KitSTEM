using System.ComponentModel.DataAnnotations;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class CreateCategoryRequest
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
