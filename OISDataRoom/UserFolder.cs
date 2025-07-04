using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class UserFolder
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid DesktopSyncCategoryId { get; set; }

    public Guid EmailCategoryId { get; set; }

    public Guid CategoryId { get; set; }

    public string? Email { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? UserSign { get; set; }

    public string? UserWatermark { get; set; }

    public bool IsSplashScreen { get; set; }

    public string? CompanySeal { get; set; }
}
