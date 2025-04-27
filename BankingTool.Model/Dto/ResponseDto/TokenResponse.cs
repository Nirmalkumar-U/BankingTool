namespace BankingTool.Model.Dto.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public List<ClaimDto> Claims { get; set; }
        public string ExpireIn { get; set; }
        public List<GetActionsByUserIdDto> ActionPaths { get; set; }

    }
}
