using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Actor;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Actor")]
    [CustomActionFilters]
    public class ActorController : Controller 
    {
        private readonly IActorService _actorService;
        private readonly IHostingEnvironment _host;

        public ActorController(IActorService actorService, IHostingEnvironment host)
        {
            _actorService = actorService;
            _host = host;
        }

        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var result = await _actorService.GetActors();
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("photo/{photoName}")]
        public async Task<IActionResult> GetActorPhoto(string photoName)
        {
            if (photoName == null || photoName == "null")
                return BadRequest();

            var stream = _host.WebRootPath + "\\ActorsPictures\\" + photoName;
            var imageFileStream = System.IO.File.OpenRead(stream);
            return File(imageFileStream, "image/jpeg");
        }

        [HttpGet("{actorId}")]
        public async Task<IActionResult> GetActor(int actorId)
        {
            var result = await _actorService.GetActor(actorId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //[HttpGet("assign/{actorId}/{tvShowId}")]
        //public async Task<IActionResult> AssignActor(int actorId, int tvShowId)
        //{
        //    var result = await _actorService.AssignActorToTvShow(actorId, tvShowId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}

        //[HttpDelete("assign/{actorId}/{tvShowId}")]
        //public async Task<IActionResult> RemoveActorAssignment(int actorId, int tvShowId)
        //{
        //    var result = await _actorService.RemoveActorAssignment(actorId, tvShowId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> AddActor([FromForm] AddActorBindingModel addActorBindingModel)
        {
            var result = await _actorService.AddActor(addActorBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActor(int actorId)
        {
            var result = await _actorService.DeleteActor(actorId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
