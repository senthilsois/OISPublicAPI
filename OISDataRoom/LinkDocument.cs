using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class LinkDocument
{
    public Guid Id { get; set; }

    public Guid LinkId { get; set; }

    public Guid DocumentId { get; set; }

    public bool IsLinked { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? LinkName { get; set; }
}
