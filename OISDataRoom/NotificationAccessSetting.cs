using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class NotificationAccessSetting
{
    public Guid Id { get; set; }

    public string? IsDefaultNotificationId { get; set; }

    public bool? IsEmail { get; set; }

    public bool? IsSms { get; set; }

    public bool? IsWhatsapp { get; set; }

    public bool? IsWeb { get; set; }

    public int? CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? ClientId { get; set; }
}
