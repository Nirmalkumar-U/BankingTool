namespace BankingTool.Model
{
    public class Users : BaseSchema
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public bool IsActive { get; set; }


        // Optional Navigation Properties
        public virtual City City { get; set; } 
        public virtual State State { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
