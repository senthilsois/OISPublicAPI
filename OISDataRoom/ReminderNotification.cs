using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class ReminderNotification
{
    public Guid Id { get; set; }

    public Guid ReminderId { get; set; }

    public string? Subject { get; set; }

    public string? Description { get; set; }

    public DateTime FetchDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsEmailNotification { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
