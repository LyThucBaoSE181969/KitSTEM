using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Component
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<KitComponent> KitComponents { get; set; } = new List<KitComponent>();

    public virtual Type Type { get; set; } = null!;
}
