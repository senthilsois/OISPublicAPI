using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class EmailSyncSetting
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string SecretKey { get; set; } = null!;

    public Guid ClientId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsDefault { get; set; }

    public bool IsEnableSsl { get; set; }

    public string? Name { get; set; }

    public int? CompanyId { get; set; }

    public string? SsoclientId { get; set; }
}
