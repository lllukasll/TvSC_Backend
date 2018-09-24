using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TvSC.Data.BindingModels;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.Keys;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public AccountService(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
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

        public async Task<ResponseDto<LoginDto>> Login(LoginBindingModel model)
        {
            var response = new ResponseDto<LoginDto>();
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
    }
}
