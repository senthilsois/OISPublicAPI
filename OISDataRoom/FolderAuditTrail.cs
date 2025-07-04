using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderAuditTrail
{
    public Guid Id { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public Guid CategoryId { get; set; }

    public int OperationName { get; set; }

    public string ActionName { get; set; } = null!;

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

    public Guid? AssignToUserId1 { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public Guid? AssignToRoleId1 { get; set; }

    public string? RoleName { get; set; }

    public string? FolderType { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryType { get; set; }
}
