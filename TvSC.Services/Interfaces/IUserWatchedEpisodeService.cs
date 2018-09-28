using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.WatchedEpisodes;

namespace TvSC.Services.Interfaces
{
    public interface IUserWatchedEpisodeService
    {
        Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int episodeId, string userId);
        Task<ResponseDto<BaseModelDto>> DeleteUserWatchedEpisode(int watchedEpisodeId);
        Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodes(string userId);
        Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodesForTvSeries(string userId, int tvSeriesId);
    }
}