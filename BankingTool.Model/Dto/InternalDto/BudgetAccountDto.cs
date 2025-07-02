using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.InternalDto
{
    public class BudgetAccountDto : RequestId
    {
        public int Amount { get; set; }
    }
}
