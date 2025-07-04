using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DataRoomUser
{
    public Guid Id { get; set; }

    public Guid? DataRoomId { get; set; }

    public Guid? UserId { get; set; }

    public string? AccessLevel { get; set; }

    public DateTime? LastActive { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public string? CreatedByName { get; set; }
}
