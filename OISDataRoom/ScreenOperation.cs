using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class ScreenOperation
{
    public Guid Id { get; set; }

    public Guid OperationId { get; set; }

    public Guid ScreenId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool? Default { get; set; }

    public virtual Operation Operation { get; set; } = null!;

    public virtual Screen Screen { get; set; } = null!;
}
