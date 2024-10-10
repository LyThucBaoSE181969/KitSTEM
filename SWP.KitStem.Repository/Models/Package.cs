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
    [Table("Package")]
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int KitId { get; set; }
        public int LevelId { get; set; }
        [Required]
        public int Price { get; set; }
        public bool Status { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; } = null!;

        [ForeignKey("KitId")]
        [InverseProperty("Packages")]
        public virtual Kit Kit { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Package")]
        public virtual ICollection<Cart> Carts { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Package")]
        public virtual ICollection<PackageLab> PackageLabs { get; set; } = null!;
    }
}
