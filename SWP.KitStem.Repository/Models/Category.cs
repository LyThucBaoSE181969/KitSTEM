using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Kit> Kits { get; set; } = new List<Kit>();
}
