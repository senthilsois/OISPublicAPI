using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class EmailSmtpsetting
{
    public Guid Id { get; set; }

    public string Host { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsEnableSsl { get; set; }

    public int Port { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }
}
