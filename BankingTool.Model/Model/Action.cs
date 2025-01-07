namespace BankingTool.Model
{
    public class Action : BaseSchema
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionType { get; set; }
        public string Access { get; set; }
        public int MenuLevel { get; set; }
        public int ParrentMenuId { get; set; }

        //public ICollection<RoleAccess> RoleAccesses { get; set; }
    }
}
