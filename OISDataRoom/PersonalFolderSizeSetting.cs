using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class PersonalFolderSizeSetting
{
    public Guid Id { get; set; }

    public int? PersonaltotalSpace { get; set; }

    public int? CompanyId { get; set; }

    public int? LocationId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid ModifiedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
