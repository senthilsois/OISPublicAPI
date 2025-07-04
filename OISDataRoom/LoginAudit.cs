using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class LoginAudit
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public DateTime LoginTime { get; set; }

    public string? RemoteIp { get; set; }

    public string? Status { get; set; }

    public string? Provider { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }
}
