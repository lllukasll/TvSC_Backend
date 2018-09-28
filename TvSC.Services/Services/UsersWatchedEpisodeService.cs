using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.FavouriteTvSeries;
using TvSC.Data.DtoModels.WatchedEpisodes;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class UsersWatchedEpisodeService : IUserWatchedEpisodeService
    {
        private readonly IRepository<UserWatchedEpisode> _userWatchedEpisodeRepository;
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IMapper _mapper;

        public UsersWatchedEpisodeService(IRepository<UserWatchedEpisode> userWatchedEpisodeRepository,IRepository<Episode> episodeRepository, IMapper mapper)
        {
            _userWatchedEpisodeRepository = userWatchedEpisodeRepository;
            _episodeRepository = episodeRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodesForTvSeries(string userId, int tvSeriesId)
        {
            var response = new ResponsesDto<WatchedEpisodesResponseDto>();

            var watchedEpisodesList = _userWatchedEpisodeRepository.GetAllBy(x => x.UserId == userId && x.Episode.Season.TvShowId == tvSeriesId, x => x.Episode.Season.TvShow);
            List<WatchedEpisodesResponseDto> mappedEpisodesList = new List<WatchedEpisodesResponseDto>();

            foreach (var watchedEpisode in watchedEpisodesList)
            {
                mappedEpisodesList.Add(_mapper.Map<WatchedEpisodesResponseDto>(watchedEpisode));
            }

            response.DtoObject = mappedEpisodesList;

            return response;
        }

        public async Task<ResponsesDto<WatchedEpisodesResponseDto>> GetUserWatchedEpisodes(string userId)
        {
            var response = new ResponsesDto<WatchedEpisodesResponseDto>();

            var watchedEpisodesList = _userWatchedEpisodeRepository.GetAllBy(x => x.UserId == userId, x => x.Episode.Season.TvShow);
            List<WatchedEpisodesResponseDto> mappedEpisodesList = new List<WatchedEpisodesResponseDto>();

            foreach (var watchedEpisode in watchedEpisodesList)
            {
                mappedEpisodesList.Add(_mapper.Map<WatchedEpisodesResponseDto>(watchedEpisode));
            }

            response.DtoObject = mappedEpisodesList;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int episodeId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var episodeExists = await _episodeRepository.ExistAsync(x => x.Id == episodeId);
            if (!episodeExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var watchedTvSeriesInDb = await _userWatchedEpisodeRepository.GetByAsync(x => x.EpisodeId == episodeId && x.UserId == userId);
            if (watchedTvSeriesInDb != null)
            {
                response.AddError(Model.WatchedEpisode, Error.watchedEpisode_Already_Exists);
                return response;
            }

            UserWatchedEpisode watchedEpisode = new UserWatchedEpisode();
            watchedEpisode.EpisodeId = episodeId;
            watchedEpisode.UserId = userId;

            var result = await _userWatchedEpisodeRepository.AddAsync(watchedEpisode);

            if (!result)
            {
                response.AddError(Model.FavouriteTvShow, Error.favouriteTvShow_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteUserWatchedEpisode(int watchedEpisodeId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var episodeExists = await _userWatchedEpisodeRepository.ExistAsync(x => x.Id == watchedEpisodeId);
            if (!episodeExists)
            {
                response.AddError(Model.Episode, Error.episode_NotFound);
                return response;
            }

            var watchedEpisode = await _userWatchedEpisodeRepository.GetByAsync(x => x.Id == watchedEpisodeId);

            var result = await _userWatchedEpisodeRepository.Remove(watchedEpisode);

            if (!result)
            {
                response.AddError(Model.WatchedEpisode, Error.watchedEpisode_Deleting);
                return response;
            }

            return response;
        }
    }
}
