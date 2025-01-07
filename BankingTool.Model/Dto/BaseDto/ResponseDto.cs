namespace BankingTool.Model
{
    public class ResponseDto<T>
    {
        public List<string> Message { get; set; }
        public T Result { get; set; }
        public bool Status { get; set; }
    }
}
