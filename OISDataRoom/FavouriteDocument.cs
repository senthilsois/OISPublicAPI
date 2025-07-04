using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class FavouriteDocument
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Name { get; set; }

    public Guid? DocumentId { get; set; }

    public int? Favourite { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }
}
