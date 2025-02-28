namespace BankingTool.Model.Dto.BankAccount
{
    public class TransferAmountInitialLoadDto
    {
        public List<DropDownDto> FromAccount { get; set; }
        public List<DropDownDto> ToAccount { get; set; }
    }
}
