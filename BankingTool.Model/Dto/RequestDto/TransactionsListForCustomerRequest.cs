namespace BankingTool.Model.Dto.RequestDtos
{
    public class TransactionsListForCustomerRequestObject
    {
        public TransactionsListForCustomerRequest Request { get; set; }
    }
    public class TransactionsListForCustomerRequest
    {
        public TransactionsListForCustomerRequestCustomer Customer { get; set; }
        public TransactionsListForCustomerRequestAccount Account { get; set; }
        public TransactionsListForCustomerRequestBank Bank { get; set; }
    }
    public class TransactionsListForCustomerRequestBank : RequestId
    {
    }
    public class TransactionsListForCustomerRequestAccount : RequestId
    {
    }
    public class TransactionsListForCustomerRequestCustomer : RequestId
    {
    }
}
