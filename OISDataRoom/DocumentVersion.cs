using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentVersion
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string? Url { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? Comments { get; set; }

    public Guid? CloudId { get; set; }

    public double? Version { get; set; }

    public string? FilePath { get; set; }

    public string? CloudSlug { get; set; }

    public double? DocumentSize { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
