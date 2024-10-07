using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Cart
{
    public string UserId { get; set; } = null!;

    public int PackageId { get; set; }

    public int PackageQuantity { get; set; }

    public virtual Package Package { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
