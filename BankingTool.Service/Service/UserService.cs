using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.User;
using BankingTool.Model.Dto.Response;
using BankingTool.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Service
{
    public class UserService(IUserRepository userRepository, IEncriptDecriptService encriptDecriptService,
        IRoleRepository roleRepository, ICommonRepository commonRepository,
        IConfiguration configuration) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEncriptDecriptService _encriptDecriptService = encriptDecriptService;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ICommonRepository _commonRepository = commonRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ResponseDto<LoggedInUserResponse>> Login(string email, string password)
        {
            var response = new ResponseDto<LoggedInUserResponse>
            {
                StatuCode = 200,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };
            var errors = new List<Errors>();

            var (user, role) = await _userRepository.GetUserAndRoleByEmailId(email);
            if (user == null)
            {
                errors.Add(new Errors { ErrorMessage = "There is no user for this Email: " + email, PropertyName = "Email" });
            }
            else
            {
                var pass = _encriptDecriptService.DecryptData(user.Password);
                if (pass == password)
                {
                    response.Status = true;
                    response.Message = "Login Successfully...";
                    var loggedUser = new LoggedInUserResponse()
                    {
                        UserId = user.UserId,
                        Email = email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RoleId = role.RoleId
                    };
                    response.Response = loggedUser;
                }
                else
                {
                    errors.Add(new Errors { ErrorMessage = "Password is incorrect: " + email, PropertyName = "Password" });
                }
            }
            if (errors.Count != 0)
            {
                response.StatuCode = 400;
                response.Status = false;
                response.Errors = [.. errors];
            }
            return response;
        }
        public async Task<ResponseDto<TokenResponse>> CreateToken(CreateTokenRequestUser user, int roleId)
        {
            var response = new ResponseDto<TokenResponse>
            {
                Status = false,
                Errors = [],
                ValidationErrors = []
            };

            var securityKey = _configuration["AppSettings:SecurityKey"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            var issuer = _configuration["AppSettings:Issuer"];
            var audience = _configuration["AppSettings:Audience"];
            var expiresInSeconds = Convert.ToInt32(_configuration["AppSettings:ExpiresIn"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var actions = await _userRepository.GetAllActionIdOfRole(roleId);
            var customer = await _userRepository.GetCustomerByUserId(user.Id);
            Staff staff = await _userRepository.GetStaffByUserId(user.Id);
            var act = string.Join(",", actions);

            var claims = new List<ClaimDto>
                    {
                        new() { Key = AppClaimTypes.UserId, Value = user.Id.ToString()},
                        new() { Key = AppClaimTypes.FirstName, Value = user.FirstName},
                        new() { Key = AppClaimTypes.LastName, Value = user.LastName },
                        new() { Key = AppClaimTypes.EmailId, Value = user.Email },
                        new() { Key = AppClaimTypes.RoleId, Value = roleId.ToString()},
                        new() { Key = AppClaimTypes.Actions, Value = act},
                        new() { Key = AppClaimTypes.StaffId, Value = staff != null? staff.StaffId.ToString():""},
                        new() { Key = AppClaimTypes.CustomerId, Value = customer != null? customer.CustomerId.ToString():""}
                    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value)), "Custom"),
                IssuedAt = DateTime.Now,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256)
            };
            tokenDescriptor.Expires = DateTime.Now.AddSeconds(expiresInSeconds);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var actionPaths = await _userRepository.GetActionsByUserId(user.Id);
            var accessToken = tokenHandler.WriteToken(token);

            response.Response = new()
            {
                AccessToken = accessToken,
                Claims = claims,
                ExpireIn = expiresInSeconds.ToString(),
                ActionPaths = actionPaths
            };
            response.StatuCode = 200;
            response.Status = true;
            return response;
        }
        public async Task<ResponseDto<UserInitialLoadResponse>> GetUserInitialLoad(int? userId)
        {
            var response = new ResponseDto<UserInitialLoadResponse>
            {
                Status = false,
                ValidationErrors = [],
                Errors = []
            };

            var (user, role) = userId.HasValue ? await _userRepository.GetUserAndRoleByUserId(userId.Value) : (new Users(), new Role());

            response.Response = new UserInitialLoadResponse
            {
                UserDetail = new UserDetailResponse
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    Password = user.UserId != 0 ? _encriptDecriptService.DecryptData(user.Password) : null,
                    City = user.City,
                    State = user.State,
                    RoleId = role.RoleId
                }
            };
            List<DropDownListDto> dropDownListDtos = new List<DropDownListDto>()
            {
                new DropDownListDto { Name = "Role", DropDown = await _roleRepository.GetRoleListDropDown() },
                new DropDownListDto { Name = "State", DropDown = await _commonRepository.GetAllStateDropDownList() }
            };
            response.DropDownList.AddRange(dropDownListDtos);

            response.StatuCode = 200;
            response.Status = true;
            return response;
        }
        public async Task<ResponseDto<bool>> GetCityDropDownListByStateId(int stateId)
        {

            var response = new ResponseDto<bool>
            {
                StatuCode = 200,
                Status = true,
                DropDownList = new List<DropDownListDto>()
                {
                    new DropDownListDto { Name = "City", DropDown = await _userRepository.GetCityDropDownListByStateId(stateId) }
                },
                Response = true,
                Errors = [],
                ValidationErrors = []
            };
            return response;
        }
        public async Task<ResponseDto<int?>> InsertUser(SaveUserRequestObject user)
        {
            ResponseDto<int?> response = new()
            {
                Status = false,
                ValidationErrors = [],
                Errors = []
            };
            Users userDetail = new()
            {
                FirstName = user.Request.User.FirstName,
                LastName = user.Request.User.LastName,
                EmailId = user.Request.User.Email,
                Password = _encriptDecriptService.EncryptData(user.Request.User.Password),
                State = user.Request.State.Id,
                City = user.Request.City.Id,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            };

            UserRole userRole = new()
            {
                RoleId = user.Request.Role.Id
            };

            var role = await _roleRepository.GetRoleByRoleId(user.Request.Role.Id);
            bool isCustomerNeedToInsert;
            Staff staff = null;
            Customer customer = null;
            if (role.RoleLevel < 3)
            {
                staff = new()
                {
                    StaffLevel = 5,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                isCustomerNeedToInsert = false;
            }
            else
            {
                customer = new()
                {
                    CustomerLevel = 5,
                    PrimaryAccountNumber = null,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                isCustomerNeedToInsert = true;
            }
            int? userId = _userRepository.CreateUser(userDetail, userRole, staff, customer, isCustomerNeedToInsert);

            if (userId.HasValue)
            {
                response.Message = "User Created is successfully...";
                response.StatuCode = 200;
                response.Status = true;
                response.Response = userId.Value;
            }
            else
            {
                response.StatuCode = 400;
                response.Errors.Add(new Errors { PropertyName = "Create User", ErrorMessage = "User Created is failed" });
                response.Message = "User Created is failed...";
                response.Status = false;
                response.Response = null;
            }

            return response;
        }
        public async Task<ResponseDto<List<UserListResponse>>> GetUserList()
        {
            var userList = await _userRepository.GetUserList();
            var result = userList.Select(x => new UserListResponse
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserMailId = x.UserMailId,
                IsActive = x.IsActive,
                Password = _encriptDecriptService.DecryptData(x.Password),
                PrimaryAccountNumber = x.PrimaryAccountNumber,
                RoleName = x.RoleName,
                State = x.State,
                City = x.City
            }).ToList();
            return new ResponseDto<List<UserListResponse>>
            {
                StatuCode = 200,
                Response = result,
                Status = true,
                Errors = [],
                ValidationErrors = []
            };
        }
        public async Task<List<GetActionsByUserIdDto>> Test(int id)
        {
            var a = await _userRepository.GetActionsByUserId(id);
            return a;
        }
    }
}
