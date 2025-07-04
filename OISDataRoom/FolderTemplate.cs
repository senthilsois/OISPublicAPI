using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FolderTemplate
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? ParentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool? Permission { get; set; }

    public string? FolderPosition { get; set; }

    public string? CategoryId { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }
}
