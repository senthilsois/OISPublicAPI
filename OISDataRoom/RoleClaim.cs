using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class RoleClaim
{
    public int Id { get; set; }

    public Guid OperationId { get; set; }

    public Guid ScreenId { get; set; }

    public Guid RoleId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Operation Operation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual Screen Screen { get; set; } = null!;
}
