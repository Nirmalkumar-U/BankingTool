namespace BankingTool.Service.IService
{
    /// <summary>
    /// Service interface for credit card data encryption and decryption.
    /// Provides secure AES-256 encryption for PAN, CVV, and expiry dates.
    /// </summary>
    public interface ICreditCardCryptographyService
    {
        /// <summary>
        /// Encrypts a credit card number (PAN) using AES-256
        /// </summary>
        string EncryptCardNumber(string cardNumber);

        /// <summary>
        /// Decrypts an encrypted credit card number
        /// </summary>
        string DecryptCardNumber(string encryptedCardNumber);

        /// <summary>
        /// Encrypts a credit card CVV using AES-256
        /// </summary>
        string EncryptCVV(string cvv);

        /// <summary>
        /// Decrypts an encrypted CVV
        /// </summary>
        string DecryptCVV(string encryptedCVV);

        /// <summary>
        /// Encrypts an expiry date using AES-256
        /// </summary>
        string EncryptExpiryDate(string expiryDate);

        /// <summary>
        /// Decrypts an encrypted expiry date
        /// </summary>
        string DecryptExpiryDate(string encryptedExpiryDate);

        /// <summary>
        /// Masks a card number for display (e.g., "****-****-****-1234")
        /// </summary>
        string MaskCardNumber(string cardNumber);

        /// <summary>
        /// Masks an expiry date for display (e.g., "12/**")
        /// </summary>
        string MaskExpiryDate(string decryptedExpiryDate);
    }
}
