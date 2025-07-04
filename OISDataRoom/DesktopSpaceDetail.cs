using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DesktopSpaceDetail
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public float UsedSpace { get; set; }

    public float AvailableSpace { get; set; }

    public float AllocatedSpace { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
