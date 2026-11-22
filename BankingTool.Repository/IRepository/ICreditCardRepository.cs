using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> GetCreditCardById(int creditCardId);
        Task<List<CreditCard>> GetActiveCreditCardsByCustomerId();
        Task<int> InsertCreditCard(CreditCard creditCard);
        Task<bool> UpdateCreditCard(CreditCard creditCard);
        Task<bool> UpdateCreditLimit(int creditCardId, decimal newLimit);
        Task<bool> UpdateCreditCardStatus(int creditCardId, CreditCardStatus creditCardStatus);
    }
}
