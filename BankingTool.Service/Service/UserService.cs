﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingTool.Model;
using BankingTool.Repository;
using Microsoft.IdentityModel.Tokens;

namespace BankingTool.Service
{
    public class UserService(IUserRepository userRepository, IEncriptDecriptService encriptDecriptService, IRoleRepository roleRepository, ICommonRepository commonRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEncriptDecriptService _encriptDecriptService = encriptDecriptService;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ICommonRepository _commonRepository = commonRepository;
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

            var securityKey = "NMyISuRpeMrSAecLreHtKAeyR12I3!NI";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
            var issuer = "itismekumaru";
            var audience = "itisaudience";
            var expiresInSeconds = 900;
            var expiresIn = DateTime.Now.AddSeconds(expiresInSeconds);

            var tokenHandler = new JwtSecurityTokenHandler();
            var actions = await _userRepository.GetAllActionIdOfRole(user.RoleId);
            var act = string.Join(",", actions);

            var claims = new List<ClaimDto>
                    {
                        new ClaimDto { Key = AppClaimTypes.UserId, Value = user.UserId.ToString()},
                        new ClaimDto { Key = AppClaimTypes.FirstName, Value = user.FirstName},
                        new ClaimDto { Key = AppClaimTypes.LastName, Value = user.LastName },
                        new ClaimDto { Key = AppClaimTypes.EmailId, Value = user.Email },
                        new ClaimDto { Key = AppClaimTypes.RoleId, Value = user.RoleId.ToString()},
                        new ClaimDto { Key = AppClaimTypes.Actions, Value = act}
                    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new(AppClaimTypes.UserId, claims.Find(x=>x.Key==AppClaimTypes.UserId)?.Value),
                            new(AppClaimTypes.FirstName, claims.Find(x=>x.Key==AppClaimTypes.FirstName)?.Value),
                            new(AppClaimTypes.LastName, claims.Find(x=>x.Key==AppClaimTypes.LastName)?.Value),
                            new(AppClaimTypes.EmailId, claims.Find(x=>x.Key==AppClaimTypes.EmailId)?.Value),
                            new(AppClaimTypes.RoleId, claims.Find(x=>x.Key==AppClaimTypes.RoleId)?.Value),
                            new(AppClaimTypes.Actions, claims.Find(x=>x.Key==AppClaimTypes.Actions)?.Value)
                }),
                IssuedAt = DateTime.Now,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256)
            };

            tokenDescriptor.Expires = tokenDescriptor.IssuedAt?.AddSeconds(expiresInSeconds);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var actionPaths = await _userRepository.GetActionsByUserId(user.UserId);

            response.Result = new()
            {
                AccessToken = tokenHandler.WriteToken(token),
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

        public async Task<ResponseDto<int>> InsertUser(SaveUserDto user)
        {
            ResponseDto<int> response = new()
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
            int userId = await _userRepository.InsertUser(userDetail);

            UserRole userRole = new()
            {
                UserId = userId,
                RoleId = user.Role
            };

            bool isInsertedUserRole = await _userRepository.InsertUserRole(userRole);

            var role = await _roleRepository.GetRoleByRoleId(user.Role);
            bool isInsertedStaff = true;
            bool isInsertedCustomer = true;
            if (role.RoleLevel < 3)
            {
                Staff staff = new()
                {
                    UserId = userId,
                    StaffLevel = 5,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                isInsertedStaff = await _userRepository.InsertStaff(staff);
            }
            else
            {
                Customer customer = new()
                {
                    UserId = userId,
                    CustomerLevel = 5,
                    PrimaryAccountNumber = null,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                isInsertedCustomer = await _userRepository.InsertCustomer(customer);
            }

            if (!isInsertedUserRole && !isInsertedStaff && !isInsertedCustomer)
            {
                response.Message.Add("User Creation is failed...");
            }
            else
            {
                response.Status = true;
                response.Result = userId;
            }

            return response;
        }

        public async Task<List<GetActionsByUserIdDto>> Test(int id)
        {
            var a = await _userRepository.GetActionsByUserId(id);
            return a;
        }
    }
}
