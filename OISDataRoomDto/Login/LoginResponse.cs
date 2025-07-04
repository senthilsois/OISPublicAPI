using OISPublic.OISDataRoom;

namespace OISPublic.OISDataRoomDto
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
       public string Token { get; set; }
        public DataRoomMasterUser? User { get; set; }
    }
}
