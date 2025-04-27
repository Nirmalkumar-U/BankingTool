namespace BankingTool.Model.Dto.RequestDtos
{
    public class SaveUserRequestObject
    {
        public SaveUserRequest Request { get; set; }
    }
    public class SaveUserRequest
    {
        public SaveUserRequestUser User { get; set; }
        public SaveUserRequestState State { get; set; }
        public SaveUserRequestCity City { get; set; }
        public SaveUserRequestRole Role { get; set; }
    }
    public class SaveUserRequestUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class SaveUserRequestState : RequestId
    {
    }
    public class SaveUserRequestCity : RequestId
    {
    }
    public class SaveUserRequestRole : RequestId
    {
    }
}
