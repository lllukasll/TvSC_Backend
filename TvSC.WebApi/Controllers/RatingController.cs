using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Rating;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("Rating")]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [Authorize]
        [HttpPost("tvSeries/{tvSeriesId}")]
        public async Task<IActionResult> AddTvSeriesRating(int tvSeriesId, [FromBody] AddTvSeriesRatingBindingModel tvSeriesRatingBindingModel)
        {
            var user = User.Identity.Name;
            var result = await _ratingService.AddTvSeriesUserRating(user, tvSeriesId, tvSeriesRatingBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("tvSeries/{tvSeriesId}")]
        public async Task<IActionResult> GetTvSeriesRating(int tvSeriesId)
        {
            var result = await _ratingService.GetTvSeriesRating(tvSeriesId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("tvSeries/{tvSeriesId}/forUser")]
        public async Task<IActionResult> GetTvSeriesRatingForUser(int tvSeriesId)
        {
            var user = User.Identity.Name;
            var result = await _ratingService.GetTvSeriesRatingForUser(user, tvSeriesId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{ratingId}")]
        public async Task<IActionResult> UpdateTvSeriesRating(int ratingId,
            [FromBody] UpdateTvSeriesRatingBindingModel tvSeriesRatingBindingModel)
        {
            var user = User.Identity.Name;
            var result = await _ratingService.UpdateTvSeriesUserRating(user, ratingId, tvSeriesRatingBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeletetvSeriesRating(int ratingId)
        {
            var result = await _ratingService.DeleteTvSeriesUserRating(ratingId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
