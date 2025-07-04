using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentType
{
    public int Id { get; set; }

    public string DocType { get; set; } = null!;

    public string DocSlug { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }
}
