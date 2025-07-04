using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class ReminderScheduler
{
    public Guid Id { get; set; }

    public DateTime Duration { get; set; }

    public bool IsActive { get; set; }

    public int? Frequency { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid UserId { get; set; }

    public bool IsRead { get; set; }

    public bool IsEmailNotification { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public virtual Document? Document { get; set; }

    public virtual User User { get; set; } = null!;
}
