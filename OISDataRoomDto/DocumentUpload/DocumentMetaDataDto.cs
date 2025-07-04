using Microsoft.AspNetCore.Mvc;

namespace OISPublic.OISDataRoomDto
{
    public class DocumentMetaDataDto
    {
        public Guid? Id { get; set; }
        public Guid? DocumentId { get; set; }
        public string Metatag { get; set; }
    }
}
