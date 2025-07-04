using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DataRoomMasterUser
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsExternal { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public string? Token { get; set; }
}
