using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocCombine
{
    public Guid DocCombineId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? FolderId { get; set; }

    public string DocType { get; set; } = null!;

    public string? CloudSlug { get; set; }

    public string? ClientId { get; set; }

    public int? CompanyId { get; set; }

    public bool IsPersonal { get; set; }

    public string? Url { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? Messages { get; set; }

    public string Status { get; set; } = null!;

    public string? ModifiedBy { get; set; }

    public string? Token { get; set; }

    public string? UserId { get; set; }

    public string? ErrorMessages { get; set; }

    public virtual ICollection<DocCombineDocument> DocCombineDocuments { get; set; } = new List<DocCombineDocument>();
}
