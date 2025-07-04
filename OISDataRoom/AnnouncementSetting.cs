using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class AnnouncementSetting
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public bool? Status { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
