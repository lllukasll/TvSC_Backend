using System.Threading.Tasks;
using TvSC.Data.BindingModels.Comment;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Comment;

namespace TvSC.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ResponseDto<BaseModelDto>> AddComment(AddCommentBindingModel addCommentBindingModel, int tvSeriesId, string userId);

        Task<ResponseDto<BaseModelDto>> UpdateComment(UpdateCommentBindingModel updateCommentBindingModel,
            int commentId, string userId);
        Task<ResponsesDto<GetTvSeriesCommentsDto>> GetTvSeriesComments(int tvSeriesId);
        Task<ResponseDto<BaseModelDto>> DeleteComment(int commentId, string userId);
    }
}