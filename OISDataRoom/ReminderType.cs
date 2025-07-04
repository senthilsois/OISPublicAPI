using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class ReminderType
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
