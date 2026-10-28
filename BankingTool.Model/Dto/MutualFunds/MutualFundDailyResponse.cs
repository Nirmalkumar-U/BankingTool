using System.Text.Json.Serialization;

namespace BankingTool.Model.Dto.MutualFunds
{
    public class MutualFundDailyResponse
    {
        [JsonPropertyName("scheme_code")]
        public int SchemeCode { get; set; }

        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("isin")]
        public string Isin { get; set; }

        [JsonPropertyName("isin2")]
        public string Isin2 { get; set; }

        [JsonPropertyName("first_available_date")]
        public DateTime FirstAvailableDate { get; set; }

        [JsonPropertyName("latest_available_date")]
        public DateTime LatestAvailableDate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("data")]
        public List<NavData> Data { get; set; }
    }

    public class NavData
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("nav")]
        public decimal Nav { get; set; }
    }
}
