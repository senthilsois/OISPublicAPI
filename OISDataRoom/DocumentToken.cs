using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentToken
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public Guid Token { get; set; }

    public DateTime CreatedDate { get; set; }
}
