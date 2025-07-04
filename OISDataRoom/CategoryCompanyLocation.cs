using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class CategoryCompanyLocation
{
    public int Id { get; set; }

    public Guid? CategoryId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int TotalSpace { get; set; }

    public string? ClientId { get; set; }

    public bool IsFolderIndexing { get; set; }

    public virtual Category? Category { get; set; }
}
