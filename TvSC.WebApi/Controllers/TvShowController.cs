using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _host;

        public TvShowController(ITvShowService tvShowService, IHostingEnvironment host)
        {
            _tvShowService = tvShowService;
            _host = host;
        }

        [HttpPost("getByParameters")]
        public async Task<IActionResult> GetTvShows([FromBody] SearchByParameters parameters)
        {
            var tvShows = await _tvShowService.GetAllTvShows(parameters);
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

        [HttpGet("photo/{photoName}")]
        public async Task<IActionResult> GetTvShowPhoto(string photoName)
        {
            if (photoName == null || photoName == "null")
                return BadRequest();

            var stream = _host.WebRootPath + "\\TvShowsPictures\\" + photoName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> AddTvShow([FromForm] AddTvShowBindingModel tvShowBindingModel)
        {
            var result = await _tvShowService.AddTvShow(tvShowBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{tvShowId}")]
        public async Task<IActionResult> UpdateTvShow(int tvShowId,[FromForm] UpdateTvShowBindingModel tvShowBindingModel)
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
