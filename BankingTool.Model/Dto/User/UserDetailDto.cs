namespace BankingTool.Model
{
    public class UserDetailDto
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
