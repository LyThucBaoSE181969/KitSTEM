using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    public class Cart
    {
        public string UserId { get; set; } = null!;
        public int PackageId { get; set; }
        public int PackageQuantity { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Carts")]
        public virtual ApplicationUser User { get; set; } = null!;
        [ForeignKey("PackageId")]
        [InverseProperty("Carts")]
        public virtual Package Package { get; set; } = null!;
    }
}
