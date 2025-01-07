namespace BankingTool.Service
{
    public interface IEncriptDecriptService
    {
        public string EncryptData(string data);
        public string DecryptData(string data);
    }
}
