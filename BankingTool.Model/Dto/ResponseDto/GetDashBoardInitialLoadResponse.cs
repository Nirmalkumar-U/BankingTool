namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetDashBoardInitialLoadResponse
    {
        public List<DashBoardAccountList> DashBoardAccountList { get; set; }
        public int TotalBalance { get; set; }
    }
    public class DashBoardAccountList
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public int Balance { get; set; }
        public int AccountId { get; set; }
    }
}
