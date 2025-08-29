using System.Security.Cryptography;
using System.Text;

namespace BankingTool.Service
{
    public class EncriptDecriptService : IEncriptDecriptService
    {
        private string passphrase = "MySecretKeyDEV@D15B169D-40FC-4FCA-B95B-C8ECF23A3ED7";
        private readonly int KeyLengthBits;
        private readonly int SaltLength;
        private readonly RNGCryptoServiceProvider rng = new();

        public EncriptDecriptService()
        {
            KeyLengthBits = 256; //AES Key Length in bits
            SaltLength = 8; //Salt length in bytes

            rng = new RNGCryptoServiceProvider();
        }

        public string EncryptData(string data)
        {
            return EncryptString(data, passphrase);
        }

        public string DecryptData(string data)
        {
            return DecryptString(data, passphrase);
        }

        private string DecryptString(string ciphertext, string passphrase)
        {
            var inputs = ciphertext.Split(":".ToCharArray(), 3);
            var iv = Convert.FromBase64String(inputs[0]); // Extract the IV
            var salt = Convert.FromBase64String(inputs[1]); // Extract the salt
            var ciphertextBytes = Convert.FromBase64String(inputs[2]); // Extract the ciphertext
            byte[] plaintext = null;
            // Derive the key from the supplied passphrase and extracted salt
            byte[] key = DeriveKeyFromPassphrase(passphrase, salt);
            // Decrypt
            plaintext = DoCryptoOperation(ciphertextBytes, key, iv, false);
            return Encoding.UTF8.GetString(plaintext);
        }

        private string EncryptString(string plaintext, string passphrase)
        {
            var salt = GenerateRandomBytes(SaltLength); // Random salt
            var iv = GenerateRandomBytes(16); // AES is always a 128-bit block size
            var key = DeriveKeyFromPassphrase(passphrase, salt); // Derive the key from the passphrase
            // Encrypt
            var ciphertext = DoCryptoOperation(Encoding.UTF8.GetBytes(plaintext), key, iv, true);
            // Return the formatted string
            return string.Format("{0}:{1}:{2}", Convert.ToBase64String(iv), Convert.ToBase64String(salt), Convert.ToBase64String(ciphertext));
        }

        private byte[] DeriveKeyFromPassphrase(string passphrase, byte[] salt)
        {
            int iterationCount = 2000;
            var keyDerivationFunction = new Rfc2898DeriveBytes(passphrase, salt, iterationCount);  //PBKDF2
            return keyDerivationFunction.GetBytes(KeyLengthBits / 8);
        }

        private byte[] GenerateRandomBytes(int lengthBytes)
        {
            var bytes = new byte[lengthBytes];
            rng.GetBytes(bytes);
            return bytes;
        }

        // This function does both encryption and decryption, depending on the value of the "encrypt" parameter
        private byte[] DoCryptoOperation(byte[] inputData, byte[] key, byte[] iv, bool encrypt)
        {
            byte[] output;
            using (var aes = new AesCryptoServiceProvider())
            using (var ms = new MemoryStream())
            {
                var cryptoTransform = encrypt ? aes.CreateEncryptor(key, iv) : aes.CreateDecryptor(key, iv);
                using (var cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                    cs.Write(inputData, 0, inputData.Length);
                output = ms.ToArray();
            }
            return output;
        }
        //FIPS Methods End
    }
}
