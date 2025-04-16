using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Model
{
    public class ResponseDto<T>
    {
        public List<DropDownListDto> DropDownList { get; set; }
        public List<Errors> Errors { get; set; }
        public List<ValidationResults> ValidationErrors { get; set; }
        public T Response { get; set; }
        public bool Status { get; set; }
        public int StatuCode { get; set; }
        public string Message { get; set; }
    }

    public class Errors
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
