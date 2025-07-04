using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderSizingConfiguration
{
    public Guid Id { get; set; }

    public int? PersonalTotalSpace { get; set; }

    public int? EmailTotalSpace { get; set; }

    public int? DesktopSyncTotalSpace { get; set; }

    public float? PersonalEachUser { get; set; }

    public float? EmailEachUser { get; set; }

    public float? DesktopSyncEachUser { get; set; }

    public int? PersonalUsedSpace { get; set; }

    public int? EmailTotalUsedSpace { get; set; }

    public int? DesktopSyncUsedSpace { get; set; }

    public int? CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int? UserCounts { get; set; }

    public int? EmailUserCount { get; set; }

    public int? DesktopUserCount { get; set; }

    public string? ClientId { get; set; }
}
