using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("tvShow")]
    [CustomActionFilters]
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

        [HttpGet("{tvSeriesId}")]
        public async Task<IActionResult> GetTvShow(int tvSeriesId)
        {
            var result = await _tvShowService.GetTvSeries(tvSeriesId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTvShow([FromBody] AddTvShowBindingModel tvShowBindingModel)
        {
            var result = await _tvShowService.AddTvShow(tvShowBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{tvShowId}")]
        public async Task<IActionResult> UpdateTvShow(int tvShowId,[FromBody] UpdateTvShowBindingModel tvShowBindingModel)
        {
            var result = await _tvShowService.UpdateTvShow(tvShowId, tvShowBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{tvShowId}")]
        public async Task<IActionResult> DeleteTvShow(int tvShowId)
        {
            var result = await _tvShowService.DeleteTvShow(tvShowId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
