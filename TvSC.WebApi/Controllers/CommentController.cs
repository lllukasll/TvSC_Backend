using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Comment;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Comment")]
    [CustomActionFilters]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("{tvSeriesId}")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentBindingModel addCommentBindingModel, int tvSeriesId)
        {
            var user = User.Identity.Name;
            var result = await _commentService.AddComment(addCommentBindingModel, tvSeriesId, user);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{tvSeriesId}")]
        public async Task<IActionResult> GetTvSeriesComments(int tvSeriesId)
        {
            var result = await _commentService.GetTvSeriesComments(tvSeriesId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var user = User.Identity.Name;
            var result = await _commentService.DeleteComment(commentId, user);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentBindingModel updateCommentBindingModel,
            int commentId)
        {
            var user = User.Identity.Name;
            var result = await _commentService.UpdateComment(updateCommentBindingModel, commentId, user);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
