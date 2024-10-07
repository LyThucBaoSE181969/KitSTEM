using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class KitComponent
{
    public int KitId { get; set; }

    public int ComponentId { get; set; }

    public int ComponentQuantity { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual Kit Kit { get; set; } = null!;
}
