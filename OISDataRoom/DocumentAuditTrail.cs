using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentAuditTrail
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public int OperationName { get; set; }

    public Guid? AssignToUserId { get; set; }

    public Guid? AssignToRoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? Body { get; set; }

    public string? Type { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
