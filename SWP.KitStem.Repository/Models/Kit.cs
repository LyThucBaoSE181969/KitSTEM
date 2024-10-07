using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Kit
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Brief { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int PurchaseCost { get; set; }

    public bool Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<KitComponent> KitComponents { get; set; } = new List<KitComponent>();

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
