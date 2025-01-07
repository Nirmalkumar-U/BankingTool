namespace BankingTool.Model
{
    public class State : BaseSchema
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Abbreviation { get; set; }
    }
}
