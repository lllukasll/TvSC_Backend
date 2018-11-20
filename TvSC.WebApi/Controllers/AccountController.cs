using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TvSC.Data.BindingModels;
using TvSC.Data.DbModels;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Account")]
    [Authorize]
    [CustomActionFilters]
    public class AccountController : BaseResponseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("getUserByCookie")]
        public async Task<IActionResult> GetUserByCookie()
        {
            var userId = User.Identity.Name;
            var result = await _accountService.GetUserByCookie(userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors());
            }

            var result = await _accountService.Register(model);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            //await _emailService.SendEmail(model.Email, "Platforma Browaru - aktywacja",
            //    $"Witaj {model.UserName}!\n Aby aktywować swoje konto kliknij w poniższy link:\n http://localhost:18831/Users/Activate/" +
            //    user.Guid);

            return Ok(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors());
            }

            var result = await _accountService.Login(model);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("LogOut")]
        public async Task LogOut()
        {
            await _accountService.LogOut();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrors());
            }

            var userId = User.Identity.Name;
            var result = await _accountService.ChangePassword(userId, model);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
