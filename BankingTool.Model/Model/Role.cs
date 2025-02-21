namespace BankingTool.Model
{
    public class Role : BaseSchema
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public int RoleLevel { get; set; }
    }
}
