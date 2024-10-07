using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class LabSupport
{
    public Guid Id { get; set; }

    public Guid OrderSupportId { get; set; }

    public string StaffId { get; set; } = null!;

    public int Rating { get; set; }

    public string? FeedBack { get; set; }

    public bool IsFinished { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Guid LabSupportId { get; set; }

    public virtual OrderSupport OrderSupport { get; set; } = null!;

    public virtual AspNetUser Staff { get; set; } = null!;
}
