using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class UserFolderDragAndDrop
{
    public Guid? Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? FolderId { get; set; }

    public Guid? ParentId { get; set; }

    public int? CompanyId { get; set; }

    public string? FolderPosition { get; set; }

    public string? ClientId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public bool Pinned { get; set; }
}
