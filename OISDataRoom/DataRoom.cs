using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class DataRoom
{
    public Guid Id { get; set; }

    public Guid? CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int Status { get; set; }

    public string? DefaultPermission { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string ClientId { get; set; } = null!;

    public int CompanyId { get; set; }

    public int Storage { get; set; }

    public string? CompanyName { get; set; }
    public ICollection<DataRoomUser> DataRoomUsers { get; set; }
    public ICollection<DataRoomFile> DataRoomFiles { get; set; }
}
