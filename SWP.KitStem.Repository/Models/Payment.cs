using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class Payment
{
    public Guid Id { get; set; }

    public int MethodId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public bool Status { get; set; }

    public int Amount { get; set; }

    public virtual Method Method { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
