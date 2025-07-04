using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class TLocation
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? Complist { get; set; }

    public bool? DeleteFlag { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? OnShoreOffShore { get; set; }

    public int TotalSpace { get; set; }
}
