using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Season;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Seasons")]
    [CustomActionFilters]
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet("{tvSeriesId}")]
        public async Task<IActionResult> GetSeasons(int tvSeriesId)
        {
            var result = await _seasonService.GetAllSeasons(tvSeriesId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetSeason(int seasonId)
        {
            var result = await _seasonService.GetSeason(seasonId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{tvSeriesId}")]
        public async Task<IActionResult> AddSeason(int tvSeriesId, [FromBody] AddSeasonBindingModel seasonBindingModel)
        {
            var result = await _seasonService.AddSeason(tvSeriesId, seasonBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{seasonId}")]
        public async Task<IActionResult> UpdateSeason(int seasonId,[FromBody] UpdateSeasonBindingModel seasonBindingModel)
        {
            var result = await _seasonService.UpdateSeason(seasonId, seasonBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{seasonId}")]
        public async Task<IActionResult> DeleteSeason(int seasonId)
        {
            var result = await _seasonService.DeleteSeason(seasonId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
