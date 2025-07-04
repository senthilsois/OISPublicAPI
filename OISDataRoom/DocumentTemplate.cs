using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DocumentTemplate
{
    public Guid Id { get; set; }

    public string? CategoryId { get; set; }

    public string? DocumentName { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? DocumentTypeId { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }

    public double? DocumentSize { get; set; }

    public string? DocumentType { get; set; }

    public bool Status { get; set; }

    public string? FilePath { get; set; }

    public int? CompanyId { get; set; }

    public string? ClientId { get; set; }

    public string? CreatedbyUsername { get; set; }
}
