namespace BankingTool.Model.Dto.RequestDtos
{
    public class BankDropDownListRequestObject
    {
        public BankDropDownListRequest Request { get; set; }
    }
    public class BankDropDownListRequest
    {
        public BankDropDownListRequestCustomer Customer { get; set; }
    }
    public class BankDropDownListRequestCustomer : RequestId
    {
    }
}
