using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class KitCreateRequest
    {
        [Required]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "phải đặt tên cho kit")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "phải ghi mô tả ngắn cho kit")]
        public string Brief { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Giá mua phải lớn hơn hoặc bằng 0.")]
        public int PurchaseCost { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public List<IFormFile>? KitImagesList { get; set; }
    }
}
