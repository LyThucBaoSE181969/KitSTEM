using Microsoft.EntityFrameworkCore;
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
    [Table("Lab")]
    public class Lab
    {
        [Key]
        public Guid Id { get; set; }

        public int LevelId { get; set; }

        public int KitId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Unicode(false)]
        public string Url { get; set; } = null!;

        public int Price { get; set; }

        public int MaxSupportTimes { get; set; }

        [StringLength(100)]
        public string? Author { get; set; }

        public bool Status { get; set; }

        [ForeignKey("KitId")]
        [InverseProperty("Labs")]
        public virtual Kit Kit { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Lab")]
        public virtual ICollection<OrderSupport>? OrderSupports { get; set; }

        [ForeignKey("LevelId")]
        [InverseProperty("Labs")]
        public virtual Level Level { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Lab")]
        public virtual ICollection<PackageLab> PackageLabs { get; set; } = null!;
    }
}
