using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("LabSupport")]
    public class LabSupport
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderSupportId { get; set; }
        public string StaffId { get; set; } = null!;
        public int Rating { get; set; }
        public string? FeedBack { get; set; }
        public bool IsFinished { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        [ForeignKey("LabSupportId")]
        [InverseProperty("LabSupports")]
        public virtual OrderSupport OrderSupport { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("StaffId")]
        [InverseProperty("LabSupports")]
        public virtual ApplicationUser Staff { get; set; } = null!;
    }
}
