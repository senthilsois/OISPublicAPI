namespace OISPublic.OISDataRoomDto
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsExternal { get; set; }
    }


    public class EncryptedLoginRequest
    {
        public string EncryptedData { get; set; } = string.Empty;
        public string EncryptedKey { get; set; } = string.Empty;
        public string EncryptedIV { get; set; } = string.Empty;
    }

}
