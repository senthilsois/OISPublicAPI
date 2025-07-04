using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class ShareLinkPermission
{
    public Guid Id { get; set; }

    public string ShareType { get; set; } = null!;

    public string ShareLink { get; set; } = null!;

    public string ShareKey { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? ShareId { get; set; }
}
