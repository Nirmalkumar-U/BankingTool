namespace BankingTool.Model
{
    public class DropDownDto
    {
        public int? Key { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; } = false;
    }

    public class DropDownListDto
    {
        public string Name { get; set; }
        public List<DropDownDto> DropDown { get; set; }
    }
}
