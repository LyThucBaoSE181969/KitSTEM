using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Level
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
