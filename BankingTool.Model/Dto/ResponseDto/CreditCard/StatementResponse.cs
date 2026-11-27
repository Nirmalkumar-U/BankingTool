using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class StatementResponseObject
    {
        public StatementResponse Response { get; set; }
    }
    public class StatementResponseListObject
    {
        public List<StatementResponse> Responses { get; set; }
    }

    public class StatementResponse
    {
        public StatementResponseStatement Statement { get; set; }
    }

    public class StatementResponseStatement : RequestId
    {
        public DateTime BillingCycleStartDate { get; set; }
        public DateTime BillingCycleEndDate { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal Interest { get; set; }
        public decimal Fees { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal CardBalance { get; set; }
        public decimal MinimumDueAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string StatementStatus { get; set; }
        public List<StatementResponseStatementDetail> StatementDetails { get; set; } = new List<StatementResponseStatementDetail>();
    }

    public class StatementResponseStatementDetail : RequestId
    {
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
