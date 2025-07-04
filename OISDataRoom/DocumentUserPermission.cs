using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentUserPermission
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public Guid UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsTimeBound { get; set; }

    public bool IsAllowDownload { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? StartTime { get; set; }

    public string? PasswordHash { get; set; }

    public bool? IsPassword { get; set; }

    public Guid? ShareId { get; set; }

    public string? ShareType { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? CreatedbyUsername { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
