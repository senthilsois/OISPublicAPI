using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class QuarterlyReminder
{
    public Guid Id { get; set; }

    public Guid ReminderId { get; set; }

    public int Day { get; set; }

    public int Month { get; set; }

    public int Quarter { get; set; }

    public virtual Reminder Reminder { get; set; } = null!;
}
