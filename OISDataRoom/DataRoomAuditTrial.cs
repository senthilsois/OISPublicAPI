using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DataRoomAuditTrial
{
    public Guid Id { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public Guid? DataRoomId { get; set; }

    public int? Operation { get; set; }

    public string? ActionName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DataRoomName { get; set; }

    public Guid? UserId { get; set; }

    public string? DocumentName { get; set; }
}
