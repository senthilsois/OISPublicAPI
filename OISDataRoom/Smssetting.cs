using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Smssetting
{
    public Guid Id { get; set; }

    public string? AuthToken { get; set; }

    public string? AuthKey { get; set; }

    public string? MobileNumber { get; set; }

    public string? AuthUrl { get; set; }

    public int? CompanyId { get; set; }

    public bool? IsDefault { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? ClientId { get; set; }
}
