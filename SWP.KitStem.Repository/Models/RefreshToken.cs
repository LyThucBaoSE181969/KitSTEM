using System;
using System.Collections.Generic;

namespace SWP.KitStem.Repository.Models;

public partial class RefreshToken
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime ExpirationTime { get; set; }
}
