using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Order
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

    public virtual ICollection<OrderSupport> OrderSupports { get; set; } = new List<OrderSupport>();

    public virtual ICollection<PackageOrder> PackageOrders { get; set; } = new List<PackageOrder>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
