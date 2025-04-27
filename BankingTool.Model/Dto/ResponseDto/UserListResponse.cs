namespace BankingTool.Model.Dto.Response
{
    public class UserListResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserMailId { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string PrimaryAccountNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
    }
}
