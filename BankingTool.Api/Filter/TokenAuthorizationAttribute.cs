using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using BankingTool.Repository;
using BankingTool.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace BankingTool.Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class TokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
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
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = false,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = mySecurityKey,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromMinutes(10)
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validateParameters, out var validatedToken);
                context.HttpContext.User = principal;
                List<ClaimDto> claims = JsonSerializer.Deserialize<List<ClaimDto>>(context.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == nameof(ClaimTypes.Email))?.Value);

                var expireAtClaim = claims.FirstOrDefault(x => x.Key == AppClaimTypes.ExpiresAt)?.Value;
                if (!string.IsNullOrEmpty(expireAtClaim) && DateTime.TryParse(expireAtClaim, out DateTime expireAt))
                {
                    var now = DateTime.UtcNow;

                    if (expireAt < now)
                    {
                        var expiredDuration = now - expireAt;

                        if (expiredDuration.TotalMinutes > 5)
                        {
                            context.Result = new ContentResult
                            {
                                StatusCode = StatusCodes.Status401Unauthorized,
                                Content = "Token expired."
                            };
                        }
                        else
                        {
                            context.Result = new ContentResult
                            {
                                StatusCode = 498, // Custom code for token expired, refresh required
                                Content = "Token expired, refresh required."
                            };
                        }
                    }
                }
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
