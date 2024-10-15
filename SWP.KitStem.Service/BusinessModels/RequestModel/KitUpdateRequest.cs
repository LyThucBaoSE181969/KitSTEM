using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class KitUpdateRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Không tìm thấy sản phẩm")]
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Không tìm thấy sản phẩm")]
        public int CategoryId { get; set; } = 1;
        [Required(ErrorMessage = "Tên không được để trống.")]
        [StringLength(100)]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Tóm tắt không được để trống.")]
        public string Brief { get; set; } = "";
        [Required]
        public string? Description { get; set; } = "";
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Giá mua phải lớn hơn hoặc bằng 0.")]
        public int PurchaseCost { get; set; } = 1;
        public List<IFormFile>? KitImagesList { get; set; }
    }
}
