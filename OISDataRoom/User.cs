using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class User
{
    public Guid Id { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? EmailCategoryId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? DesktopSyncCategoryId { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? SecurityStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string? UserName { get; set; }

    public string? AccessFailedCount { get; set; }

    public string? DialCode { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool IsDeleted { get; set; }

    public long PersonalSpaceUsage { get; set; }

    public string? UserWatermark { get; set; }

    public string? UserSign { get; set; }

    public virtual ICollection<DesktopSpaceDetail> DesktopSpaceDetails { get; set; } = new List<DesktopSpaceDetail>();

    public virtual ICollection<DocumentAuditTrail> DocumentAuditTrails { get; set; } = new List<DocumentAuditTrail>();

    public virtual ICollection<DocumentComment> DocumentComments { get; set; } = new List<DocumentComment>();

    public virtual ICollection<DocumentRolePermission> DocumentRolePermissions { get; set; } = new List<DocumentRolePermission>();

    public virtual ICollection<DocumentUserPermission> DocumentUserPermissions { get; set; } = new List<DocumentUserPermission>();

    public virtual ICollection<DocumentVersion> DocumentVersions { get; set; } = new List<DocumentVersion>();

    public virtual ICollection<ReminderScheduler> ReminderSchedulers { get; set; } = new List<ReminderScheduler>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
