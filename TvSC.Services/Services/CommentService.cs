using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Comment;
using TvSC.Data.BindingModels.Notification;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Comment;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly INotificationService _notificationService;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<TvShow> _tvSeriesRepository;
        private readonly IMapper _mapper;

        public CommentService(INotificationService notificationService ,IRepository<Comment> commentRepository, IRepository<TvShow> tvSeriesRepository, IMapper mapper)
        {
            _notificationService = notificationService;
            _commentRepository = commentRepository;
            _tvSeriesRepository = tvSeriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<BaseModelDto>> AddComment(AddCommentBindingModel addCommentBindingModel,
            int tvSeriesId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            if (userId == null)
            {
                response.AddError(Model.Account, Error.account_Login);
                return response;
            }

            Comment comment = new Comment();
            comment.Content = addCommentBindingModel.Content;
            comment.TvSeriesId = tvSeriesId;
            comment.UserId = userId;
            comment.CreateDateTime = DateTime.Now;
            comment.UpdateDateTime = DateTime.Now;

            var result = await _commentRepository.AddAsync(comment);
            if (!result)
            {
                response.AddError(Model.Comment, Error.comment_Adding);
                return response;
            }
            else
            {
                if (userId != null)
                {
                    AddNotificationBindingModel addNotificationBindingModel = new AddNotificationBindingModel();
                    addNotificationBindingModel.Type = "commentedTvSeries";

                    await _notificationService.AddNotification(addNotificationBindingModel, tvSeriesId, userId);
                }
            }

            return response;
        }

        public async Task<ResponsesDto<GetTvSeriesCommentsDto>> GetTvSeriesComments(int tvSeriesId)
        {
            var response = new ResponsesDto<GetTvSeriesCommentsDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var comments = _commentRepository.GetAllBy(x => x.TvSeriesId == tvSeriesId, x => x.User).OrderByDescending(x => x.CreateDateTime);
            if (comments.Any())
            {
                List<GetTvSeriesCommentsDto> tvSeriesComments = new List<GetTvSeriesCommentsDto>();
                foreach (var comment in comments)
                {
                    tvSeriesComments.Add(_mapper.Map<GetTvSeriesCommentsDto>(comment));
                }

                response.DtoObject = tvSeriesComments;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteComment(int commentId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var commentExists = await _commentRepository.ExistAsync(x => x.Id == commentId);
            if (!commentExists)
            {
                response.AddError(Model.Comment, Error.tvShow_NotFound);
                return response;
            }

            if (userId == null)
            {
                response.AddError(Model.Actor, Error.account_Login);
                return response;
            }

            var comment = await _commentRepository.GetByAsync(x => x.Id == commentId);

            if (comment.UserId != userId)
            {
                response.AddError(Model.Comment, Error.comment_Author);
                return response;
            }

            if (comment != null)
            {
                var result = await _commentRepository.Remove(comment);
                if (!result)
                {
                    response.AddError(Model.Comment, Error.comment_Deleting);
                    return response;
                }
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> UpdateComment(UpdateCommentBindingModel updateCommentBindingModel,
            int commentId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var commentExists = await _commentRepository.ExistAsync(x => x.Id == commentId);
            if (!commentExists)
            {
                response.AddError(Model.Comment, Error.tvShow_NotFound);
                return response;
            }

            if (userId == null)
            {
                response.AddError(Model.Actor, Error.account_Login);
                return response;
            }

            var comment = await _commentRepository.GetByAsync(x => x.Id == commentId);
            comment.Content = updateCommentBindingModel.Content;
            comment.UpdateDateTime = DateTime.Now;

            if (comment.UserId != userId)
            {
                response.AddError(Model.Comment, Error.comment_Author);
                return response;
            }

            var result = await _commentRepository.UpdateAsync(comment);
            if (!result)
            {
                response.AddError(Model.Category, Error.comment_Updating);
                return response;
            }

            return response;
        }
    }
}
