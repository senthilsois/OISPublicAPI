using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class VersionScreen
{
    public Guid Id { get; set; }

    public string? VersionNumber { get; set; }

    public string? VersionTitle { get; set; }

    public string? VersionDescription1 { get; set; }

    public string? VersionDescription2 { get; set; }

    public string? VersionDescription3 { get; set; }

    public string? VersionDescription4 { get; set; }

    public string? VersionDescription5 { get; set; }

    public bool? VersionIsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
