using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentShareLinkPermission
{
    public Guid Id { get; set; }

    public Guid ShareId { get; set; }

    public string DocumentId { get; set; } = null!;

    public string ShareType { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? ShareLink { get; set; }

    public string? ShareKey { get; set; }
}
