using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderIndexingScheme
{
    public Guid Id { get; set; }

    public int? SchemeNum { get; set; }

    public string SchemeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
