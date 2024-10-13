using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public int MethodId { get; set; }
        public Guid OrderId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool Status { get; set; }
        public long Amount { get; set; }
        [ForeignKey("MethodId")]
        [InverseProperty("Payments")]
        public virtual Method Method { get; set; } = null!;
        [ForeignKey("OrderId")]
        [InverseProperty("Payment")]
        public virtual UserOrders? UserOrders { get; set; }
    }
}
