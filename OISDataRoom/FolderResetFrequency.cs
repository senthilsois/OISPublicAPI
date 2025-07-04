using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderResetFrequency
{
    public Guid Id { get; set; }

    public Guid FolderId { get; set; }

    public Guid FolderIndexingId { get; set; }

    public bool IsYearly { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime MonthlyConfig { get; set; }

    public bool IsMonthly { get; set; }

    public DateTime YearlyConfig { get; set; }

    public bool? IsResetDone { get; set; }

    public bool IsActive { get; set; }
}
