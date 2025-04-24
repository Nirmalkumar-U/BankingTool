namespace BankingTool.Model.Dto.RequestDtos
{
    public class GetTransferAmountInitialLoadRequestObject
    {
        public GetTransferAmountInitialLoadRequest Request { get; set; }
    }
    public class GetTransferAmountInitialLoadRequest
    {
        public GetTransferAmountInitialLoadRequestCustomer Customer { get; set; }
    }
    public class GetTransferAmountInitialLoadRequestCustomer : RequestId
    {
    }
}
