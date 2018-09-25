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
        [HttpPost("{tvSeriesId}")]
        public async Task<IActionResult> AddTvSeriesRating(int tvSeriesId, [FromBody] AddTvSeriesRatingBindingModel tvSeriesRatingBindingModel)
        {
            var user = User.Identity.Name;
            var result = await _ratingService.AddTvSeriesRating(user, tvSeriesRatingBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
