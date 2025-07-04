using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentsDetail
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = null!;

    public string? ExtractedText { get; set; }

    public string? PreprocessedText { get; set; }

    public DateTime UploadedAt { get; set; }

    public int PageNumber { get; set; }

    public Guid ParentId { get; set; }

    public string? Tags { get; set; }
}
