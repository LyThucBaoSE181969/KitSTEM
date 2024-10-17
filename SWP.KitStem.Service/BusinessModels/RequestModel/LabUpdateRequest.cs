using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels.RequestModel
{
    public class LabUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int KitId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [StringLength(100)]
        public string? Author { get; set; }
        [Required]
        public IFormFile? File { get; set; }
    }
}
