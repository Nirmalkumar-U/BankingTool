namespace BankingTool.Model
{
    public class AppSettingsDto
    {
        public string ApiKey { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiresIn { get; set; }
        public string IsEncryptionEnabled { get; set; }
        public string FieldsToEncrypt { get; set; }
    }
}
