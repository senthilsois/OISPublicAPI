using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Document
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public float? DocumentSize { get; set; }

    public string? Status { get; set; }

    public string? DocType { get; set; }

    public int? IsCheckedIn { get; set; }

    public int? IsCheckedOut { get; set; }

    public string? DocumentType { get; set; }

    public int ViewCount { get; set; }

    public Guid? CloudId { get; set; }

    public bool Archive { get; set; }

    public string? FilePath { get; set; }

    public string? CloudSlug { get; set; }

    public bool Purge { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public string? DocumentNumber { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<DocumentAuditTrail> DocumentAuditTrails { get; set; } = new List<DocumentAuditTrail>();

    public virtual ICollection<DocumentComment> DocumentComments { get; set; } = new List<DocumentComment>();

    public virtual ICollection<DocumentMetaData> DocumentMetaData { get; set; } = new List<DocumentMetaData>();

    public virtual ICollection<DocumentRolePermission> DocumentRolePermissions { get; set; } = new List<DocumentRolePermission>();

    public virtual ICollection<DocumentUserPermission> DocumentUserPermissions { get; set; } = new List<DocumentUserPermission>();

    public virtual ICollection<DocumentVersion> DocumentVersions { get; set; } = new List<DocumentVersion>();

    public virtual ICollection<ReminderScheduler> ReminderSchedulers { get; set; } = new List<ReminderScheduler>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<SendEmail> SendEmails { get; set; } = new List<SendEmail>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
