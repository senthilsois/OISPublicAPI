namespace OISPublic.OISDataRoomDto.DocumentUpload
{
    public class DeleteMultipleRequest
    {
        public List<Guid> DocumentIds { get; set; }
        public Guid DataRoomId { get; set; }
        public Guid DeletedBy { get; set; }
    }

}
