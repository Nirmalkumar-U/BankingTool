namespace BankingTool.Model
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Optional Navigation Properties
        public virtual Users User { get; set; } 
        public virtual Role Role { get; set; }
    }
}
