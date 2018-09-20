using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TvSC.Data.BindingModels;
using TvSC.Data.DbModels;

namespace TvSC.WebApi.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] AccountRegisterBindingModel registerBindingModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userIdentity = _mapper.Map(registerBindingModel);
        //    var result = await _userManager.CreateAsync(userIdentity, registerBindingModel.Password);

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest("problem przy result w Register/AccountController");
        //    }

        //    return Ok();
        //}
    }
}
