using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class EmailRule
{
    public Guid Id { get; set; }

    public string? Subject { get; set; }

    public string? FromAddress { get; set; }

    public string? Domain { get; set; }

    public Guid UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? ToAddress { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? CategoryId { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public string? Condition { get; set; }
}
