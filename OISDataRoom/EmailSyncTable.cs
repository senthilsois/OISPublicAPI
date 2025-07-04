using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class EmailSyncTable
{
    public Guid Id { get; set; }

    public string MessageId { get; set; } = null!;

    public string? Subject { get; set; }

    public string? SenderEmail { get; set; }

    public string? SenderName { get; set; }

    public DateTimeOffset? ReceivedDateTime { get; set; }

    public string? Body { get; set; }

    public string? Attachments { get; set; }

    public Guid UserId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? RuleId { get; set; }

    public Guid? DocumentId { get; set; }

    public string? FolderName { get; set; }
}
