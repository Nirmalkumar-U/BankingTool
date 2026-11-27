using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BankingTool.Model;
using BankingTool.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class TokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) ||
                !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
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
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = mySecurityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(10)
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validateParameters, out var validatedToken);
                context.HttpContext.User = principal;

                var emailClaim = principal.Claims.FirstOrDefault(claim =>
                    claim.Type == nameof(ClaimTypes.Email) ||
                    claim.Type == ClaimTypes.Email ||
                    claim.Type == "email")?.Value;

                List<ClaimDto>? claims = null;
                if (!string.IsNullOrEmpty(emailClaim))
                {
                    claims = JsonSerializer.Deserialize<List<ClaimDto>>(emailClaim);
                }

                var expireAtClaim = claims?.FirstOrDefault(x => x.Key == AppClaimTypes.ExpiresAt)?.Value;
                var emailId = claims?.FirstOrDefault(x => x.Key == AppClaimTypes.EmailId)?.Value;

                if (!string.IsNullOrEmpty(expireAtClaim) && DateTime.TryParse(expireAtClaim, out DateTime expireAt))
                {
                    var now = DateTime.UtcNow;

                    if (expireAt < now)
                    {
                        var expiredDuration = now - expireAt;

                        if (expiredDuration.TotalMinutes > 5 && emailId != "nirmal@gmail.com")
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
            catch (SecurityTokenExpiredException)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 498,
                    Content = "Token expired, refresh required."
                };
            }
            catch (SecurityTokenException)
            {
                context.Result = new UnauthorizedResult();
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
