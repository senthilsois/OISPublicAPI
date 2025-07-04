using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class UserQuery
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? App { get; set; }

    public string TicketNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Status { get; set; }

    public Guid UserId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
