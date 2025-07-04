using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class UserClaim
{
    public int Id { get; set; }

    public Guid OperationId { get; set; }

    public Guid ScreenId { get; set; }

    public Guid UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Operation Operation { get; set; } = null!;

    public virtual Screen Screen { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
