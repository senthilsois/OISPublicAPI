using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class TCompanyMaster
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string Address3 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public string? Division { get; set; }

    public decimal? MaxAirTicketBalance { get; set; }

    public string? CountryCode { get; set; }

    public bool? IsActive { get; set; }

    public bool? DeleteFlag { get; set; }

    public int? DefaultProbationDays { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? PayerCrnoC { get; set; }

    public string? EmployerCrnoC { get; set; }

    public string? Vertical { get; set; }

    public bool? IsTmultiProject { get; set; }

    public int TotalSpace { get; set; }
}
