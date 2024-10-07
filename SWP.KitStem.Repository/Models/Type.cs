using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Type
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
