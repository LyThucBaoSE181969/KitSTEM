using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Method
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NormalizedName { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
