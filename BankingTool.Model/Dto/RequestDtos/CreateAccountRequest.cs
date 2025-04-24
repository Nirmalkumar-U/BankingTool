namespace BankingTool.Model.Dto.RequestDtos
{
    public class CreateAccountRequestObject
    {
        public CreateAccountRequest Request { get; set; }
    }
    public class CreateAccountRequest
    {
        public CreateAccountRequestBank Bank { get; set; }
        public CreateAccountRequestAccount Account { get; set; }
        public CreateAccountRequestCustomer Customer { get; set; }
    }
    public class CreateAccountRequestBank : RequestId
    {
    }
    public class CreateAccountRequestCustomer : RequestId
    {
        public bool CustomerWantCreditCard { get; set; }
    }
    public class CreateAccountRequestAccount
    {
        public int AccountTypeId { get; set; }
        public bool DoYouWantToChangeThisAccountToPrimaryAccount { get; set; }
    }
}
