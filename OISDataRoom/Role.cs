using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Role
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<RoleCompanyLocation> RoleCompanyLocations { get; set; } = new List<RoleCompanyLocation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
