using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Assignment;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("Assignments")]
    public class ActorsAssignmentsController : Controller
    {
        private readonly IActorAssignmentService _actorAssignmentService;

        public ActorsAssignmentsController(IActorAssignmentService actorAssignmentService)
        {
            _actorAssignmentService = actorAssignmentService;
        }

        [HttpGet("tvShow/{tvShowId}")]
        public async Task<IActionResult> GetTvShowAssignments(int tvShowId)
        {
            var result = await _actorAssignmentService.GetTvShowAssignments(tvShowId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("actor/{actorId}")]
        public async Task<IActionResult> GetActorAssignments(int actorId)
        {
            var result = await _actorAssignmentService.GetActorAssignments(actorId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] AddAssignmentBindingModel addAssignmentBindingModel)
        {
            var result = await _actorAssignmentService.AssignActorToTvShow(addAssignmentBindingModel.actorId, addAssignmentBindingModel.tvShowId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{assingmentId}")]
        public async Task<IActionResult> DeleteAssignment(int assingmentId)
        {
            var result = await _actorAssignmentService.RemoveActorAssignment(assingmentId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
