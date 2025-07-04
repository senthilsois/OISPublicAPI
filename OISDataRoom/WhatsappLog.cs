using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class WhatsappLog
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? DocumentId { get; set; }

    public string? Name { get; set; }

    public long? PhoneNumber { get; set; }

    public string? Body { get; set; }

    public string? ClientId { get; set; }

    public string? CompanyId { get; set; }

    public int? ApplicationId { get; set; }

    public int? SelectedCompany { get; set; }
}
