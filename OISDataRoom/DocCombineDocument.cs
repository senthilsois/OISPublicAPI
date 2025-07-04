using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocCombineDocument
{
    public Guid Id { get; set; }

    public Guid DocCombineId { get; set; }

    public Guid DocumentId { get; set; }

    public virtual DocCombine DocCombine { get; set; } = null!;
}
