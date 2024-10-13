using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("PackageOrder")]
    public class PackageOrder
    {
        public int PackageId { get; set; }

        public Guid OrderId { get; set; }

        public int PackageQuantity { get; set; }

        [ForeignKey("OrderId")]
        public virtual UserOrders Order { get; set; } = null!;

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; } = null!;
    }
}
