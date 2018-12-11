using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Stats")]
    [CustomActionFilters]
    public class StatsController : Controller
    {
        private readonly IStateService _statsService;

        public StatsController(IStateService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserStats()
        {
            string user = User.Identity.Name;
            var response = await _statsService.GetUserStats(user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
