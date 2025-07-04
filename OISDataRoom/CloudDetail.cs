using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class CloudDetail
{
    public Guid Id { get; set; }

    public string? AuthUrl { get; set; }

    public string? AuthToken { get; set; }

    public string? AuthType { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    public string? AuthName { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }
}
