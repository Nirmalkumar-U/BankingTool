using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
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
