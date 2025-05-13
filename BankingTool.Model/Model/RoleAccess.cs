namespace BankingTool.Model
{
    public class RoleAccess
    {
        public int RoleId { get; set; }
        public int ActionId { get; set; }

        // Optional Navigation Properties
        public virtual Role Role { get; set; } 
        public virtual Action Action { get; set; }
    }
}
