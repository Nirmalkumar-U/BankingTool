using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.InternalDto
{
    public class BudgetAccountDto : RequestId
    {
        public decimal Amount { get; set; }
    }
}
