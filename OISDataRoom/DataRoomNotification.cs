using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OISPublic.OISDataRoom;

    public partial class DataRoomNotification
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? UserId { get; set; }

        public string? Title { get; set; }
        public string? Message { get; set; }

        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public string? ClientId { get; set; }
        public int? CompanyId { get; set; }
    public Guid? DataRoomId { get; set; }
    public string? Type { get; set; }
    }

