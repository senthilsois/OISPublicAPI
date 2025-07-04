using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DataRoomFile
{
    public Guid Id { get; set; }

    public Guid DataRoomId { get; set; }

    public Guid DocumentId { get; set; }

    public DateTime UploadedAt { get; set; }

    public Guid UploadedBy { get; set; }

    public string Permission { get; set; } = null!;

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public string? CreatedByName { get; set; }
}
