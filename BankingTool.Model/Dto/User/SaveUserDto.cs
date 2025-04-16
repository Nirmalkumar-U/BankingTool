namespace BankingTool.Model
{
    public class SaveUserRequestObject
    {
        public SaveUserRequest SaveUserRequest { get; set; }
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
    public class SaveUserRequestState
    {
        public int StateId { get; set; }
    }
    public class SaveUserRequestCity
    {
        public int CityId { get; set; }
    }
    public class SaveUserRequestRole
    {
        public int RoleId { get; set; }
    }
}
