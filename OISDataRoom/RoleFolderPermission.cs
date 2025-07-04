using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class RoleFolderPermission
{
    public Guid Id { get; set; }

    public Guid? RoleId { get; set; }

    public Guid? FolderId { get; set; }

    public DateTime? PeriodStartDate { get; set; }

    public DateTime? PeriodEndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsAllowDocumentUpload { get; set; }

    public bool IsAllowShare { get; set; }

    public bool IsTimeBound { get; set; }

    public bool IsShareAllDocuments { get; set; }

    public bool IsAllowSubFolderCreate { get; set; }

    public bool IsShareSubFolder { get; set; }

    public bool IsDefault { get; set; }

    public bool ShareAllDocumentWithSubfolder { get; set; }

    public bool AllowDocumentUploadWithSubfolder { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public Guid? DocumentId { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public string? RoleName { get; set; }

    public string? CreatedbyUsername { get; set; }
}
