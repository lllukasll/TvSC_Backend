using System.Threading.Tasks;
using TvSC.Data.DtoModels;

namespace TvSC.Services.Interfaces
{
    public interface IUserWatchedEpisodeService
    {
        Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int episodeId, string userId);
        Task<ResponseDto<BaseModelDto>> DeleteUserWatchedEpisode(int watchedEpisodeId);
    }
}