using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class SendEmail
{
    public Guid Id { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public string? FromEmail { get; set; }

    public Guid? DocumentId { get; set; }

    public bool IsSend { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Document? Document { get; set; }
}
