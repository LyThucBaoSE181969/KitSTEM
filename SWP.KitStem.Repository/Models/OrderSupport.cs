using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class OrderSupport
{
    public Guid Id { get; set; }

    public Guid LabId { get; set; }

    public Guid OrderId { get; set; }

    public int PackageId { get; set; }

    public int RemainSupportTimes { get; set; }

    public virtual Lab Lab { get; set; } = null!;

    public virtual ICollection<LabSupport> LabSupports { get; set; } = new List<LabSupport>();

    public virtual Order Order { get; set; } = null!;

    public virtual Package Package { get; set; } = null!;
}
