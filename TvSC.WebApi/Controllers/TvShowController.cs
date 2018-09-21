using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("tvShow")]
    [Authorize]
    public class TvShowController : Controller
    {
        private readonly ITvShowService _tvShowService;
        public TvShowController(ITvShowService tvShowService)
        {
            _tvShowService = tvShowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTvShows()
        {
            var tvShows = await _tvShowService.GetAllTvShows();
            return Ok(tvShows);
        }
    }
}
