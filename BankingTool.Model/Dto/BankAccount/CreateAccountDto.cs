namespace BankingTool.Model.Dto.BankAccount
{
    public class CreateAccountDto
    {
        public int BankId { get; set; }
        public int AccountTypeId { get; set; }
        public int CustomerId { get; set; }
        public bool CustomerWantCreditCard { get; set; }
        public bool DoYouWantToChangeThisAccountToPrimaryAccount { get; set; }
    }
}
