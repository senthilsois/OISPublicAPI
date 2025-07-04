using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Invoice
{
    public Guid Id { get; set; }

    public string? CompanyName { get; set; }

    public DateOnly? Date { get; set; }

    public string? Address { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? Phone { get; set; }

    public DateOnly? InvoiceDate { get; set; }

    public string? SupplierName { get; set; }

    public string? ModeOfPayment { get; set; }

    public string? FileName { get; set; }

    public decimal? TotalAmount { get; set; }
}
