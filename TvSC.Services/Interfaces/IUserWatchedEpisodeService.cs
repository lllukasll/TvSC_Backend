using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.DtoModels.WatchedEpisodes;

namespace TvSC.Services.Interfaces
{
    public interface IUserWatchedEpisodeService
    {
        Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int episodeId, string userId);
        Task<ResponseDto<BaseModelDto>> DeleteUserWatchedEpisode(int watchedEpisodeId, string userId);
        Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodes(string userId);
        Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodesForTvSeries(string userId, int tvSeriesId);
        Task<bool> CheckIfEpisodeWatched(int episodeId, string userId);
        Task<ResponseDto<BaseModelDto>> AddSeasonToWatched(int seasonId, string userId);
        Task<ResponseDto<BaseModelDto>> MarkSeasonAsNotWatched(int seasonId, string userId);
        Task<ResponsesDto<SeasonDto>> GetWeatchedSeasons(int tvSeriesId, string userId);
        Task<ResponsesDto<WatchedEpisodesDto>> GetLastWatchedEpisodes(string userId, int numberOfEpisodes);
        Task<ResponsesDto<TvShowDto>> GetPropositions(string userId);
    }
}