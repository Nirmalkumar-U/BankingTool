using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    /// <summary>
    /// Service for encrypting and decrypting sensitive credit card data (PAN, CVV, expiry date).
    /// Uses AES-256 encryption with PBKDF2 key derivation.
    /// </summary>
    public class CreditCardCryptographyService : ICreditCardCryptographyService
    {
        private readonly string _encryptionKey;
        private const int KeyLengthBits = 256; // AES-256
        private const int SaltLength = 16; // 16 bytes salt
        private const int IvLength = 16; // 16 bytes IV for AES
        private const int Iterations = 10000; // PBKDF2 iterations

        public CreditCardCryptographyService(IConfiguration configuration)
        {
            // Get encryption key from configuration or use default for development
            _encryptionKey = configuration?["Encryption:CreditCardKey"] 
                ?? "CreditCard-Encryption-Key-DEV@D15B169D-40FC-4FCA-B95B-C8ECF23A3ED7";
        }

        /// <summary>
        /// Encrypts credit card PAN (Primary Account Number)
        /// </summary>
        public string EncryptCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new ArgumentException("Card number cannot be null or empty", nameof(cardNumber));

            return EncryptString(cardNumber, _encryptionKey);
        }

        /// <summary>
        /// Decrypts credit card PAN
        /// </summary>
        public string DecryptCardNumber(string encryptedCardNumber)
        {
            if (string.IsNullOrEmpty(encryptedCardNumber))
                throw new ArgumentException("Encrypted card number cannot be null or empty", nameof(encryptedCardNumber));

            return DecryptString(encryptedCardNumber, _encryptionKey);
        }

        /// <summary>
        /// Encrypts credit card CVV
        /// </summary>
        public string EncryptCVV(string cvv)
        {
            if (string.IsNullOrEmpty(cvv))
                throw new ArgumentException("CVV cannot be null or empty", nameof(cvv));

            return EncryptString(cvv, _encryptionKey);
        }

        /// <summary>
        /// Decrypts credit card CVV
        /// </summary>
        public string DecryptCVV(string encryptedCVV)
        {
            if (string.IsNullOrEmpty(encryptedCVV))
                throw new ArgumentException("Encrypted CVV cannot be null or empty", nameof(encryptedCVV));

            return DecryptString(encryptedCVV, _encryptionKey);
        }

        /// <summary>
        /// Encrypts expiry date
        /// </summary>
        public string EncryptExpiryDate(string expiryDate)
        {
            if (string.IsNullOrEmpty(expiryDate))
                throw new ArgumentException("Expiry date cannot be null or empty", nameof(expiryDate));

            return EncryptString(expiryDate, _encryptionKey);
        }

        /// <summary>
        /// Decrypts expiry date
        /// </summary>
        public string DecryptExpiryDate(string encryptedExpiryDate)
        {
            if (string.IsNullOrEmpty(encryptedExpiryDate))
                throw new ArgumentException("Encrypted expiry date cannot be null or empty", nameof(encryptedExpiryDate));

            return DecryptString(encryptedExpiryDate, _encryptionKey);
        }

        /// <summary>
        /// Masks card number to show only last 4 digits (e.g., "****-****-****-1234")
        /// </summary>
        public string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
                return "****";

            var lastFourDigits = cardNumber.Substring(cardNumber.Length - 4);
            return $"****-****-****-{lastFourDigits}";
        }

        /// <summary>
        /// Masks expiry date to MM/YY format without full decryption
        /// </summary>
        public string MaskExpiryDate(string decryptedExpiryDate)
        {
            if (string.IsNullOrEmpty(decryptedExpiryDate) || decryptedExpiryDate.Length < 4)
                return "**/**";

            // Assume format is MM/YY or MMYY
            var cleanedDate = decryptedExpiryDate.Replace("/", "");
            if (cleanedDate.Length < 4)
                return "**/**";

            var month = cleanedDate.Substring(0, 2);
            var year = cleanedDate.Substring(cleanedDate.Length - 2);
            return $"{month}/**";
        }

        /// <summary>
        /// Core encryption method using AES-256
        /// </summary>
        private string EncryptString(string plaintext, string passphrase)
        {
            try
            {
                // Generate random salt and IV
                byte[] salt = new byte[SaltLength];
                byte[] iv = new byte[IvLength];

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                    rng.GetBytes(iv);
                }

                // Derive key from passphrase using PBKDF2
                byte[] key = DeriveKeyFromPassphrase(passphrase, salt);

                // Encrypt the plaintext
                byte[] ciphertextBytes;
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plaintext);
                        }
                        ciphertextBytes = ms.ToArray();
                    }
                }

                // Combine salt, IV, and ciphertext, then convert to Base64
                byte[] result = new byte[SaltLength + IvLength + ciphertextBytes.Length];
                Buffer.BlockCopy(salt, 0, result, 0, SaltLength);
                Buffer.BlockCopy(iv, 0, result, SaltLength, IvLength);
                Buffer.BlockCopy(ciphertextBytes, 0, result, SaltLength + IvLength, ciphertextBytes.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Encryption failed.", ex);
            }
        }

        /// <summary>
        /// Core decryption method using AES-256
        /// </summary>
        private string DecryptString(string ciphertext, string passphrase)
        {
            try
            {
                // Convert from Base64 and extract salt, IV, and ciphertext
                byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);

                if (ciphertextBytes.Length < SaltLength + IvLength)
                    throw new InvalidOperationException("Invalid ciphertext format.");

                byte[] salt = new byte[SaltLength];
                byte[] iv = new byte[IvLength];
                byte[] actualCiphertext = new byte[ciphertextBytes.Length - SaltLength - IvLength];

                Buffer.BlockCopy(ciphertextBytes, 0, salt, 0, SaltLength);
                Buffer.BlockCopy(ciphertextBytes, SaltLength, iv, 0, IvLength);
                Buffer.BlockCopy(ciphertextBytes, SaltLength + IvLength, actualCiphertext, 0, actualCiphertext.Length);

                // Derive key from passphrase using PBKDF2
                byte[] key = DeriveKeyFromPassphrase(passphrase, salt);

                // Decrypt the ciphertext
                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(actualCiphertext))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Decryption failed.", ex);
            }
        }

        /// <summary>
        /// Derives a 256-bit key from a passphrase using PBKDF2
        /// </summary>
        private byte[] DeriveKeyFromPassphrase(string passphrase, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KeyLengthBits / 8);
            }
        }
    }
}
