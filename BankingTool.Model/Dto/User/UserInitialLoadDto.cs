namespace BankingTool.Model
{
    public class UserInitialLoadDto
    {
        public List<DropDownDto> RoleDropDown { get; set; }
        public List<DropDownDto> StateDropDown { get; set; }
        public UserDetailDto UserDetail { get; set; }
    }
}
