using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentComment
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
