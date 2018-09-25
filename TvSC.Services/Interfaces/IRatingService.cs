using System.Threading.Tasks;
using TvSC.Data.BindingModels.Rating;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Rating;
using TvSC.Repo.Interfaces;

namespace TvSC.Services.Interfaces
{
    public interface IRatingService
    {
        Task<ResponseDto<BaseModelDto>> AddTvSeriesUserRating(string userLogged, int tvSeriesId,
            AddTvSeriesRatingBindingModel tvSeriesRatingBindingModel);

        Task<ResponseDto<GetTvSeriesRatingDto>> GetTvSeriesRating(int tvSeriesId);

        Task<ResponseDto<BaseModelDto>> UpdateTvSeriesUserRating(string userLogged, int ratingId,
            UpdateTvSeriesRatingBindingModel tvSeriesRatingBindingModel);

        Task<ResponseDto<BaseModelDto>> DeleteTvSeriesUserRating(int ratingId);

        Task<ResponseDto<GetTvSeriesRatingDto>> GetTvSeriesRatingForUser(string loggedUser, int tvSeriesId);
    }
}