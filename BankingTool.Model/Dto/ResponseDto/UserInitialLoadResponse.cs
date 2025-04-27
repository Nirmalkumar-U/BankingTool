namespace BankingTool.Model.Dto.Response
{
    public class UserInitialLoadResponse
    {
        public UserDetailResponse UserDetail { get; set; }
    }
    public class UserDetailResponse
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int RoleId { get; set; }
    }
}
