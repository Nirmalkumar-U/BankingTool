using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class TokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValidToken = ApplicationAppContext.HttpContext != null && ApplicationAppContext.HttpContext.User.Claims.Any();
            if (isValidToken)
            {
                return;
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        public void OnAuthorization1(AuthorizationFilterContext context)
        { 
            var authHeader = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var securityKey = ApplicationAppContext.GetConfigValue("SecurityKey");
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            var issuer = ApplicationAppContext.GetConfigValue("Issuer");
            var audience = ApplicationAppContext.GetConfigValue("Audience");

            var validateParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = mySecurityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validateParameters, out var tok);
                tok = tok;
                context.HttpContext.User = principal;
            }
            catch (SecurityTokenException ex)
            {
                context.Result = new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
