namespace BankingTool.Model.Dto.RequestDto.User
{
    public class LoginRequestObject
    {
        public LoginRequest Request { get; set; }
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
