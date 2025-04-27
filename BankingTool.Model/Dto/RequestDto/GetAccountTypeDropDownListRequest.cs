namespace BankingTool.Model.Dto.RequestDtos
{
    public class GetAccountTypeDropDownListRequestObject
    {
        public GetAccountTypeDropDownListRequest Request { get; set; }
    }
    public class GetAccountTypeDropDownListRequest
    {
        public GetAccountTypeDropDownListRequestCustomer Customer { get; set; }
        public GetAccountTypeDropDownListRequestBank Bank { get; set; }
    }
    public class GetAccountTypeDropDownListRequestCustomer : RequestId
    {
    }
    public class GetAccountTypeDropDownListRequestBank : RequestId
    {
    }
}
