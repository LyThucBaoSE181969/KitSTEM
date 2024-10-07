using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class PackageOrder
{
    public int PackageId { get; set; }

    public Guid OrderId { get; set; }

    public int PackageQuantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Package Package { get; set; } = null!;
}
