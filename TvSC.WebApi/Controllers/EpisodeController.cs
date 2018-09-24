using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Episode;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Episode")]
    [CustomActionFilters]
    public class EpisodeController : Controller 
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetEpisodes(int seasonId)
        {
            var result = await _episodeService.GetEpisodes(seasonId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("season/{seasonId}")]
        public async Task<IActionResult> AddEpisode(int seasonId, [FromBody] AddEpisodeBindingModel episodeBindingModel)
        {
            var result = await _episodeService.AddEpisode(seasonId, episodeBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{episodeId}")]
        public async Task<IActionResult> UpdateEpisode(int episodeId, [FromBody] UpdateEpisodeBindingModel episodeBindingModel)
        {
            var result = await _episodeService.UpdateEpisode(episodeId, episodeBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{episodeId}")]
        public async Task<IActionResult> DeleteEpisode(int episodeId)
        {
            var result = await _episodeService.DeleteEpisode(episodeId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
