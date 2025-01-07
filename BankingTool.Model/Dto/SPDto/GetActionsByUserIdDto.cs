namespace BankingTool.Model
{
    public class GetActionsByUserIdDto
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionPath { get; set; }
        public string ActionType { get; set; }
        public int MenuLevel { get; set; }
        public int? ParrentMenuId { get; set; }
        public int? Sequence { get; set; }
    }
}
