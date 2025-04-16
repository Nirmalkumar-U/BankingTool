namespace BankingTool.Model.Dto.RequestDtos
{
    public class LoginRequestObject
    {
        public LoginRequest LoginRequest { get; set; }
    }
    public class LoginRequest
    {
        public LoginRequestUser User { get; set; }
    }
    public class LoginRequestUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
