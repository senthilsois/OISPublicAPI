using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderIndexingConfiguration
{
    public Guid Id { get; set; }

    public Guid FolderId { get; set; }

    public bool IsActive { get; set; }

    public int? SequenceNumberStart { get; set; }

    public int? SequenceNumberCount { get; set; }

    public string SchemePattern { get; set; } = null!;

    public string SchemePreview { get; set; } = null!;

    public bool IsReset { get; set; }

    public bool IsApplyToSubfolders { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
