namespace BankingTool.Model.Model
{
    public class CodeValue
    {
        public int CodeValueId { get; set; }
        public string TypeName { get; set; }
        public int TypeCode { get; set; }
        public string CodeValue1 { get; set; }
        public string CodeValue2 { get; set; }
        public string CodeValue3 { get; set; }
        public string CodeValue1Description { get; set; }
        public string CodeValue2Description { get; set; }
        public string CodeValue3Description { get; set; }
        public bool InUse { get; set; }
        public int Sequence { get; set; }
    }

}
