using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class AwsBucket
{
    public Guid Id { get; set; }

    public string BucketName { get; set; } = null!;

    public string AccessKey { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public bool IsDefault { get; set; }

    public bool IsDelete { get; set; }

    public string? Url { get; set; }

    public string? Region { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }
}
