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
    [Table("Order")]
    public partial class UserOrders
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(450)]
        public string UserId { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? DeliveredAt { get; set; }

        public string ShippingStatus { get; set; } = null!;

        public bool IsLabDownloaded { get; set; }

        public int Price { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }

        public string? Note { get; set; }

        [JsonIgnore]
        [InverseProperty("Order")]
        public virtual ICollection<OrderSupport> OrderSupports { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Order")]
        public virtual ICollection<PackageOrder> PackageOrders { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("UserOrders")]
        public virtual ApplicationUser User { get; set; } = null!;

        [InverseProperty("UserOrders")]
        public virtual Payment? Payment { get; set; }
    }
}
