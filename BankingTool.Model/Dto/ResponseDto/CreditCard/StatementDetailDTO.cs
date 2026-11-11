namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class StatementDetailDTO
    {
        public int StatementDetailId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
