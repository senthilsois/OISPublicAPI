using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class RoleCompanyLocation
{
    public int Id { get; set; }

    public Guid? RoleId { get; set; }

    public int? CompanyId { get; set; }

    public int? LocationId { get; set; }

    public virtual Role? Role { get; set; }
}
