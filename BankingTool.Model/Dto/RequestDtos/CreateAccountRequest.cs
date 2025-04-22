namespace BankingTool.Model.Dto.RequestDtos
{
    public class CreateAccountRequestObject
    {
        public CreateAccountRequest CreateAccountRequest { get; set; }
    }
    public class CreateAccountRequest
    {
        public CreateAccountRequestBank Bank { get; set; }
        public CreateAccountRequestAccount Account { get; set; }
        public CreateAccountRequestCustomer Customer { get; set; }
    }
    public class CreateAccountRequestBank
    {
        public int BankId { get; set; }
    }
    public class CreateAccountRequestCustomer
    {
        public int CustomerId { get; set; }
        public bool CustomerWantCreditCard { get; set; }
    }
    public class CreateAccountRequestAccount
    {
        public int AccountTypeId { get; set; }
        public bool DoYouWantToChangeThisAccountToPrimaryAccount { get; set; }
    }
}
