namespace BankingTool.Model
{
    public class SaveUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int Role { get; set; }
    }
}
