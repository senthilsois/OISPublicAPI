using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Category
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

    public string? FolderSource { get; set; }

    public bool IsFolderIndexing { get; set; }

    public string? FolderPosition { get; set; }

    public virtual ICollection<CategoryCompanyLocation> CategoryCompanyLocations { get; set; } = new List<CategoryCompanyLocation>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }
}
