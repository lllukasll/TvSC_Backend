using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TvSC.Data.BindingModels;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Account;
using TvSC.Data.Keys;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountService(IHttpContextAccessor httpContext, UserManager<User> userManager, IMapper mapper)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto<BaseModelDto>> Register(RegisterBindingModel model)
        {
            var response = new ResponseDto<BaseModelDto>();
            var userFindByName = await _userManager.FindByNameAsync(model.UserName);
            if (userFindByName != null)
            {
                response.AddError(Model.Account, Error.account_UserExists);
            }

            var userFingByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userFingByEmail != null)
            {
                response.AddError(Model.Account, Error.account_EmailExists);
            }

            if (userFindByName == null && userFingByEmail == null)
            {
                var user = new User()
                {
                    Id = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Avatar = ""
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        response.AddError(Model.Account, error.Description);
                    }
                    return response;
                }
            }

            return response;

        }

        public async Task<ResponseDto<BaseModelDto>> UpdateAvatar(IFormFile avatar, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.AddError(Model.Account, Error.account_UserNotFound);
                return response;
            }
            else
            {
                if (user.Avatar != "")
                {
                    if (File.Exists(("wwwroot\\UsersPictures\\" + user.Avatar)))
                    {
                        File.Delete("wwwroot\\UsersPictures\\" + user.Avatar);
                    }
                }
                
                if (!Directory.Exists("wwwroot\\UsersPictures"))
                    Directory.CreateDirectory("wwwroot\\UsersPictures");

                var fileName = Guid.NewGuid() + Path.GetExtension(avatar.FileName);
                var filePath = Path.Combine("wwwroot\\UsersPictures", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

                user.Avatar = fileName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Errors.Any())
                {
                    foreach (var error in result.Errors)
                    {
                        response.AddError(Model.Account, error.Description);
                    }
                    return response;
                }
            }

            return response;
        }

        public async Task<ResponseDto<GetLoggedUserDto>> Login(LoginBindingModel model)
        {
            var response = new ResponseDto<GetLoggedUserDto>();
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.Login);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && checkPassword)
            {
                var identity = new ClaimsIdentity("Identity.Application");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                await _httpContext.HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

                var userFromDb = await _userManager.FindByNameAsync(user.Id);
                var mappedUser = _mapper.Map<GetLoggedUserDto>(userFromDb);

                response.DtoObject = mappedUser;

                return response;
            }

            response.AddError(Model.Account, Error.account_WrongCredentials);
            return response;
        }

        public async Task LogOut()
        {
            await _httpContext.HttpContext.SignOutAsync("Identity.Application");
        }

        public async Task<ResponseDto<BaseModelDto>> ChangePassword(string userId, ChangePasswordBindingModel model)
        {
            var response = new ResponseDto<BaseModelDto>();
            var user = await _userManager.FindByNameAsync(userId);

            if (user == null)
            {
                response.AddError(Model.Account, Error.account_UserNotFound);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    response.AddError(Model.Account, error.Description);
                }
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<GetLoggedUserDto>> GetUserByCookie(string userId)
        {
            var response = new ResponseDto<GetLoggedUserDto>();

            var user = await _userManager.FindByNameAsync(userId);

            if (user == null)
            {
                response.AddError(Model.Account, Error.account_UserNotFound);
            }

            var mappedUser = _mapper.Map<GetLoggedUserDto>(user);
            response.DtoObject = mappedUser;

            return response;
        }
    }
}
