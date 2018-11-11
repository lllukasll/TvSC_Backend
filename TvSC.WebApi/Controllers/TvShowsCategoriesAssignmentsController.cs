using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;

namespace TvSC.WebApi.Controllers
{
    [Route("CategoryAssignments")]
    public class TvShowsCategoriesAssignmentsController : Controller
    {
        private readonly ITvShowCategoriesAssignmentsService _tvShowCategoriesAssignmentsService;

        public TvShowsCategoriesAssignmentsController(ITvShowCategoriesAssignmentsService tvShowCategoriesAssignmentsService)
        {
            _tvShowCategoriesAssignmentsService = tvShowCategoriesAssignmentsService;
        }

        [HttpGet("byTvShowId/{tvShowId}")]
        public async Task<IActionResult> GetTvShowCategories(int tvShowId)
        {
            var result = await _tvShowCategoriesAssignmentsService.GetTvShowsCategories(tvShowId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{categoryAssignmentId}")]
        public async Task<IActionResult> DeleteCategoryAssignment(int categoryAssignmentId)
        {
            var result = await _tvShowCategoriesAssignmentsService.DeleteCategoryAssignment(categoryAssignmentId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("{categoryId}/{tvShowId}")]
        public async Task<IActionResult> AddCategoryAssignment(int categoryId, int tvShowId)
        {
            var result = await _tvShowCategoriesAssignmentsService.AddCategoryAssignment(categoryId, tvShowId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
