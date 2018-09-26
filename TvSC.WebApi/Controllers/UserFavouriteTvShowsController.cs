using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("tvShows/favourite")]
    [Authorize]
    public class UserFavouriteTvShowsController : BaseResponseController
    {
        private readonly IUserFavouriteTvShowsService _userFavouriteTvShowsService;
        private readonly IMapper _mapper;

        public UserFavouriteTvShowsController(IUserFavouriteTvShowsService userFavouriteTvShowsService, IMapper mapper)
        {
            _userFavouriteTvShowsService = userFavouriteTvShowsService;
            _mapper = mapper;
        }

        [HttpPost("{tvSeriesid}")]
        public async Task<IActionResult> AddUserFavouriteTvSeries(int tvSeriesid)
        {
            var user = User.Identity.Name;
            var response = await _userFavouriteTvShowsService.AddUserFavouriteTvSeries(tvSeriesid, user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserFavouriteTvSeries()
        {
            var user = User.Identity.Name;
            var response = await _userFavouriteTvShowsService.GetUserFavouriteTvSeries(user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{tvSeriesId}")]
        public async Task<IActionResult> DeleteUserFavouriteTvSeries(int tvSeriesId)
        {
            var user = User.Identity.Name;
            var response = await _userFavouriteTvShowsService.DeleteUserFavouriteTvSeries(tvSeriesId, user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}
