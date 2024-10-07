using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Package
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int KitId { get; set; }

    public int LevelId { get; set; }

    public int Price { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Kit Kit { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<OrderSupport> OrderSupports { get; set; } = new List<OrderSupport>();

    public virtual ICollection<PackageOrder> PackageOrders { get; set; } = new List<PackageOrder>();

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
}
