using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Image
{
    public Guid Id { get; set; }

    public int KitId { get; set; }

    public string Url { get; set; } = null!;

    public virtual Kit Kit { get; set; } = null!;
}
