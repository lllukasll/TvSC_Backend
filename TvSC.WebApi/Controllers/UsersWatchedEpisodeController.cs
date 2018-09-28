﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("UserWatchedEpisodes")]
    public class UsersWatchedEpisodeController : Controller
    {
        private readonly IUserWatchedEpisodeService _userWatchedEpisodeService;

        public UsersWatchedEpisodeController(IUserWatchedEpisodeService userWatchedEpisodeService)
        {
            _userWatchedEpisodeService = userWatchedEpisodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWatchedEpisodes()
        {
            var user = User.Identity.Name;
            var response = await _userWatchedEpisodeService.GetUserWatchedEpisodes(user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("forTvSeries/{tvSeriesId}")]
        public async Task<IActionResult> GetUserWatchedEpisodesForTvSeries(int tvSeriesId)
        {
            var user = User.Identity.Name;
            var response = await _userWatchedEpisodeService.GetUserWatchedEpisodesForTvSeries(user, tvSeriesId);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("{episodeId}")]
        public async Task<IActionResult> AddUserWatchedEpisode(int episodeId)
        {
            var user = User.Identity.Name;
            var response = await _userWatchedEpisodeService.AddUserFavouriteTvSeries(episodeId, user);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{watchedEpisodeId}")]
        public async Task<IActionResult> DeleteUserWatchedEpisode(int watchedEpisodeId)
        {
            var response = await _userWatchedEpisodeService.DeleteUserWatchedEpisode(watchedEpisodeId);
            if (response.ErrorOccurred)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
