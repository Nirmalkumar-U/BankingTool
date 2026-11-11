namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class StatementDTO
    {
        public int StatementId { get; set; }
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
    }
}
