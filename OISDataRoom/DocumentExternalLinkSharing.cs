using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentExternalLinkSharing
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string? ShareType { get; set; }

    public Guid ShareId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public bool IsAllowDownload { get; set; }

    public bool IsTimeBound { get; set; }

    public bool Ispassword { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
