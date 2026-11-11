using BankingTool.Model;

namespace BankingTool.Repository.IRepository
{
    public interface ICreditCardRepository
    {
        /// <summary>
        /// Gets a credit card by ID
        /// </summary>
        Task<CreditCard> GetCreditCardById(int creditCardId);

        /// <summary>
        /// Gets all credit cards for a specific customer
        /// </summary>
        Task<List<CreditCard>> GetCreditCardsByCustomerId(int customerId);

        /// <summary>
        /// Gets all active credit cards for a customer
        /// </summary>
        Task<List<CreditCard>> GetActiveCreditCardsByCustomerId();

        /// <summary>
        /// Gets credit card details by masked card number (last 4 digits)
        /// </summary>
        Task<CreditCard> GetCreditCardByCardNumber(string cardNumber);

        /// <summary>
        /// Creates a new credit card
        /// </summary>
        Task<int> InsertCreditCard(CreditCard creditCard);

        /// <summary>
        /// Updates an existing credit card
        /// </summary>
        Task<bool> UpdateCreditCard(CreditCard creditCard);

        /// <summary>
        /// Closes a credit card
        /// </summary>
        Task<bool> CloseCreditCard(int creditCardId, string reason);

        /// <summary>
        /// Updates the credit limit for a card
        /// </summary>
        Task<bool> UpdateCreditLimit(int creditCardId, decimal newLimit);

        /// <summary>
        /// Blocks a credit card
        /// </summary>
        Task<bool> BlockCreditCard(int creditCardId, string reason);

        /// <summary>
        /// Unblocks a credit card
        /// </summary>
        Task<bool> UnblockCreditCard(int creditCardId);

        /// <summary>
        /// Gets all credit cards (admin only)
        /// </summary>
        Task<List<CreditCard>> GetAllCreditCards();

        /// <summary>
        /// Deletes a credit card
        /// </summary>
        Task<bool> DeleteCreditCard(int creditCardId);
    }
}
