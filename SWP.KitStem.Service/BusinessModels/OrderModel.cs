using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.BusinessModels
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;

        public Guid PaymentId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? DeliveredAt { get; set; }

        public string ShippingStatus { get; set; } = null!;

        public bool IsLabDownloaded { get; set; }

        public int Price { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }

        public string? Note { get; set; }


        //public List<PackageOrder> PackageOrders { get; set; } = new List<PackageOrder>();

        public virtual Payment Payment { get; set; } = null!;

        public virtual AspNetUser User { get; set; } = null!;
    }
}
