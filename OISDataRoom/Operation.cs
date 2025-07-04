using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Operation
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<ScreenOperation> ScreenOperations { get; set; } = new List<ScreenOperation>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();
}
