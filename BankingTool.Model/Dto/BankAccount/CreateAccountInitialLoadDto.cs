namespace BankingTool.Model.Dto.BankAccount
{
    public class CreateAccountInitialLoadDto
    {
        public List<DropDownDto> CustomerList { get; set; }
        public List<DropDownDto> AccountTypeList { get; set; }
    }
}
