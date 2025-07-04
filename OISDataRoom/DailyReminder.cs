using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DailyReminder
{
    public Guid Id { get; set; }

    public Guid ReminderId { get; set; }

    public int DayOfWeek { get; set; }

    public bool IsActive { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
