using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [PrimaryKey("PackageId", "LabId")]
    [Table("PackageLab")]
    public class PackageLab
    {
        [Key]
        public int PackageId { get; set; }

        [Key]
        public Guid LabId { get; set; }

        [ForeignKey("LabId")]
        [InverseProperty("PackageLabs")]
        public virtual Lab Lab { get; set; } = null!;

        [ForeignKey("PackageId")]
        [InverseProperty("PackageLabs")]
        public virtual Package Package { get; set; } = null!;
    }
}
