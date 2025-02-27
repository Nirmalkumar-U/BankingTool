using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingTool.Model;
using BankingTool.Model.Dto.User;
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

        public async Task<ResponseDto<LoggedInUserDto>> Login(string email, string password)
        {
            var response = new ResponseDto<LoggedInUserDto>
            {
                Status = false,
                Message = []
            };

            var (user, role) = await _userRepository.GetUserAndRoleByEmailId(email);
            if (user == null)
            {
                response.Status = false;
                response.Message.Add("User is not Created...");
            }
            else
            {
                var pass = _encriptDecriptService.DecryptData(user.Password);
                if (pass == password)
                {
                    response.Status = true;
                    response.Message.Add("Login Successfully...");
                    var loggedUser = new LoggedInUserDto()
                    {
                        UserId = user.UserId,
                        Email = email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RoleId = role.RoleId
                    };
                    response.Result = loggedUser;
                }
                else
                {
                    response.Status = false;
                    response.Message.Add("Password is incorrect...");
                }
            }
            return response;
        }
        public async Task<ResponseDto<TokenDto>> CreateToken(LoggedInUserDto user)
        {
            var response = new ResponseDto<TokenDto> { Status = false };

            var securityKey = _configuration["AppSettings:SecurityKey"];
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            var issuer = _configuration["AppSettings:Issuer"];
            var audience = _configuration["AppSettings:Audience"];
            var expiresInSeconds = Convert.ToInt32(_configuration["AppSettings:ExpiresIn"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var actions = await _userRepository.GetAllActionIdOfRole(user.RoleId);
            var customer = await _userRepository.GetCustomerByUserId(user.UserId);
            Staff staff = await _userRepository.GetStaffByUserId(user.UserId);
            var act = string.Join(",", actions);

            var claims = new List<ClaimDto>
                    {
                        new() { Key = AppClaimTypes.UserId, Value = user.UserId.ToString()},
                        new() { Key = AppClaimTypes.FirstName, Value = user.FirstName},
                        new() { Key = AppClaimTypes.LastName, Value = user.LastName },
                        new() { Key = AppClaimTypes.EmailId, Value = user.Email },
                        new() { Key = AppClaimTypes.RoleId, Value = user.RoleId.ToString()},
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

            var actionPaths = await _userRepository.GetActionsByUserId(user.UserId);
            var accessToken = tokenHandler.WriteToken(token);

            response.Result = new()
            {
                AccessToken = accessToken,
                Claims = claims,
                ExpireIn = expiresInSeconds.ToString(),
                ActionPaths = actionPaths
            };
            response.Status = true;
            return response;
        }
        public async Task<ResponseDto<UserInitialLoadDto>> GetUserInitialLoad(int? userId)
        {
            var response = new ResponseDto<UserInitialLoadDto> { Status = false };

            var (user, role) = userId.HasValue ? await _userRepository.GetUserAndRoleByUserId(userId.Value) : (new Users(), new Role());

            var roleDropdownList = await _roleRepository.GetRoleListDropDown();
            var stateDropdownList = await _commonRepository.GetAllStateDropDownList();

            response.Result = new UserInitialLoadDto
            {
                UserDetail = new UserDetailDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    Password = user.UserId != 0 ? _encriptDecriptService.DecryptData(user.Password) : null,
                    City = user.City,
                    State = user.State,
                    RoleId = role.RoleId
                },
                RoleDropDown = roleDropdownList,
                StateDropDown = stateDropdownList
            };

            response.Status = true;
            return response;
        }
        public async Task<List<DropDownDto>> GetCityDropDownListByStateId(int stateId)
        {
            return await _userRepository.GetCityDropDownListByStateId(stateId);
        }
        public async Task<ResponseDto<int?>> InsertUser(SaveUserDto user)
        {
            ResponseDto<int?> response = new()
            {
                Status = false,
                Message = []
            };
            Users userDetail = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.Email,
                Password = _encriptDecriptService.EncryptData(user.Password),
                State = user.State,
                City = user.City,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            };

            UserRole userRole = new()
            {
                RoleId = user.Role
            };

            var role = await _roleRepository.GetRoleByRoleId(user.Role);
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
                response.Message.Add("User Created is successfully...");
                response.Status = true;
                response.Result = userId.Value;
            }
            else
            {
                response.Message.Add("User Created is failed...");
                response.Status = false;
                response.Result = null;
            }

            return response;
        }
        public async Task<ResponseDto<List<UserListDto>>> GetUserList()
        {
            var userList = await _userRepository.GetUserList();
            var result = userList.Select(x => new UserListDto
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
            return new ResponseDto<List<UserListDto>>
            {
                Message = [],
                Result = result,
                Status = true
            };
        }
        public async Task<List<GetActionsByUserIdDto>> Test(int id)
        {
            var a = await _userRepository.GetActionsByUserId(id);
            return a;
        }
    }
}
