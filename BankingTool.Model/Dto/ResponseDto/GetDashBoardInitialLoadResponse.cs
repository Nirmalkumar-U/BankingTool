namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetDashBoardInitialLoadResponse
    {
        public List<DashBoardAccountList> DashBoardAccountList { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal TotalBudgetBalance { get; set; }
    }
    public class DashBoardAccountList
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public int AccountId { get; set; }
    }
}
