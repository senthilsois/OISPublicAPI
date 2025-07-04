using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Reminder
{
    public Guid Id { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public int? Frequency { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? DayOfWeek { get; set; }

    public bool IsRepeated { get; set; }

    public bool IsEmailNotification { get; set; }

    public Guid? DocumentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? ReminderType { get; set; }

    public virtual ICollection<DailyReminder> DailyReminders { get; set; } = new List<DailyReminder>();

    public virtual Document? Document { get; set; }

    public virtual ICollection<HalfYearlyReminder> HalfYearlyReminders { get; set; } = new List<HalfYearlyReminder>();

    public virtual ICollection<QuarterlyReminder> QuarterlyReminders { get; set; } = new List<QuarterlyReminder>();

    public virtual ICollection<ReminderNotification> ReminderNotifications { get; set; } = new List<ReminderNotification>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
