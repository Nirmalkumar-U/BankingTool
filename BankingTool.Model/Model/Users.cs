namespace BankingTool.Model
{
    public class Users : BaseSchema
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public bool IsActive { get; set; }

        //public virtual UserRole UserRoles { get; set; }
    }
}
