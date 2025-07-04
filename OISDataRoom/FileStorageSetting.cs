using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FileStorageSetting
{
    public Guid Id { get; set; }

    public string Path { get; set; } = null!;

    public string PathName { get; set; } = null!;

    public int CompanyId { get; set; }

    public bool IsDefault { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string? ClientId { get; set; }
}
