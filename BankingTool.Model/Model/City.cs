namespace BankingTool.Model
{
    public class City : BaseSchema
    {
        public int CityId { get; set; }
        public int StateId { get; set; } 
        public string CityName { get; set; }
        public string PostalCode { get; set; }
    }
}
