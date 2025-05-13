namespace BankingTool.Model
{
    public class Role : BaseSchema
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public int RoleLevel { get; set; }


        // Optional Navigation Property
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<RoleAccess> RoleAccess { get; set; }
    }
}
