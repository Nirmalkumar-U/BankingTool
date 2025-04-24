namespace BankingTool.Model.Dto.RequestDtos
{
    public class CreateTokenRequestObject
    {
        public CreateTokenRequest Request { get; set; }
    }
    public class CreateTokenRequest
    {
        public CreateTokenRequestUser User { get; set; }
        public CreateTokenRequestRole Role { get; set; }
    }
    public class CreateTokenRequestUser : RequestId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class CreateTokenRequestRole : RequestId
    {
    }
}
