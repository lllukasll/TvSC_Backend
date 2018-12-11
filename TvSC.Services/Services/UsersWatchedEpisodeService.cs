using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Notification;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.FavouriteTvSeries;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.DtoModels.WatchedEpisodes;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class UsersWatchedEpisodeService : IUserWatchedEpisodeService
    {
        private readonly INotificationService _notificationService;
        private readonly IRepository<UserWatchedEpisode> _userWatchedEpisodeRepository;
        private readonly IRepository<Season> _seasonRepository;
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IMapper _mapper;
        private readonly ITvShowCategoriesAssignmentsService _tvShowCategoriesAssignmentsService;
        private readonly IRepository<TvShowCategoryAssignments> _tvShowCategoryAssignemtsRepository;
        private readonly IRepository<TvShow> _tvShowRepository;

        public UsersWatchedEpisodeService(INotificationService notificationService ,IRepository<UserWatchedEpisode> userWatchedEpisodeRepository, 
            IRepository<Season> seasonRepository, IRepository<Episode> episodeRepository, IMapper mapper, 
            ITvShowCategoriesAssignmentsService tvShowCategoriesAssignmentsService, IRepository<TvShowCategoryAssignments> tvShowCategoryAssignemtsRepository,
            IRepository<TvShow> tvShowRepository)
        {
            _notificationService = notificationService;
            _userWatchedEpisodeRepository = userWatchedEpisodeRepository;
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
            _mapper = mapper;
            _tvShowCategoriesAssignmentsService = tvShowCategoriesAssignmentsService;
            _tvShowCategoryAssignemtsRepository = tvShowCategoryAssignemtsRepository;
            _tvShowRepository = tvShowRepository;
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

        public async Task<ResponsesDto<WatchedEpisodesDto>> GetLastWatchedEpisodes(string userId, int numberOfEpisodes)
        {
            var response = new ResponsesDto<WatchedEpisodesDto>();

            var watchedEpisodesList = _userWatchedEpisodeRepository
                .GetAllBy(x => x.UserId == userId, x => x.Episode.Season.TvShow)
                .OrderByDescending(x => x.CreateDateTime)
                .Take(numberOfEpisodes);

            List<WatchedEpisodesDto> mappedWatchedEpisodes = new List<WatchedEpisodesDto>();
            foreach (var episode in watchedEpisodesList)
            {
                mappedWatchedEpisodes.Add(new WatchedEpisodesDto
                {
                    EpisodeName = episode.Episode.EpisodeName,
                    Network = episode.Episode.Season.TvShow.Network,
                    PhotoName =episode.Episode.Season.TvShow.PhotoName,
                    Id = episode.Episode.Season.TvShowId,
                    TvShowName = episode.Episode.Season.TvShow.Name
                });
            }

            response.DtoObject = mappedWatchedEpisodes;

            return response;
        }

        public async Task<ResponsesDto<TvShowDto>> GetPropositions(string userId)
        {
            var response = new ResponsesDto<TvShowDto>();

            var watchedTvShow = _userWatchedEpisodeRepository
                .GetAllBy(x => x.UserId == userId, x => x.Episode.Season.TvShow.Categories).GroupBy(x => x.Episode.Season.TvShow.Id).Select(x => x.Key);

            List<CategoryResponse> categoryList = new List<CategoryResponse>();
            foreach (var tvShowId in watchedTvShow)
            {
                var categories = await _tvShowCategoriesAssignmentsService.GetTvShowsCategories(tvShowId);
                foreach (var category in categories.DtoObject)
                {
                    categoryList.Add(category);
                }
            }

            var sortedCategoryList = categoryList.GroupBy(x => x.Name).OrderByDescending(x => x.Count()).Take(4).Select(x => x.Key).ToArray();

            List<int> tvShowsPropositions = new List<int>();

            foreach (var category in sortedCategoryList)
            {
                var bestTvShows = _tvShowCategoryAssignemtsRepository.GetAllBy(x => x.Category.Name == category)
                    .GroupBy(x => x.TvShow.Id)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key)
                    .ToList();

                foreach (var tvShow in watchedTvShow)
                {
                    bestTvShows.Remove(tvShow);
                }

                bestTvShows = bestTvShows.Take(5).ToList();
                tvShowsPropositions.AddRange(bestTvShows);
            }

            tvShowsPropositions = tvShowsPropositions.GroupBy(x => x).Select(x => x.Key).Take(15).ToList();

            List<TvShowDto> finalTvShowList = new List<TvShowDto>();

            foreach (var tvShowId in tvShowsPropositions)
            {
                var tvShow = await _tvShowRepository.GetByAsync(x => x.Id == tvShowId);
                finalTvShowList.Add(_mapper.Map<TvShowDto>(tvShow));
            }

            response.DtoObject = finalTvShowList;

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
            watchedEpisode.CreateDateTime = DateTime.Now;
            watchedEpisode.EpisodeId = episodeId;
            watchedEpisode.UserId = userId;

            var result = await _userWatchedEpisodeRepository.AddAsync(watchedEpisode);

            if (!result)
            {
                response.AddError(Model.FavouriteTvShow, Error.favouriteTvShow_Adding);
                return response;
            }
            else
            {
                AddNotificationBindingModel addNotificationBindingModel = new AddNotificationBindingModel();
                addNotificationBindingModel.Type = "watchedEpisode";

                var episode = await _episodeRepository.GetByAsync(x => x.Id == episodeId, x => x.Season.TvShow);
                addNotificationBindingModel.EpisodeNumber = episode.EpisodeNumber;

                await _notificationService.AddNotification(addNotificationBindingModel, episode.Season.TvShow.Id, userId);
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteUserWatchedEpisode(int watchedEpisodeId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var episodeExists = await _userWatchedEpisodeRepository.ExistAsync(x => x.EpisodeId == watchedEpisodeId && x.UserId == userId);
            if (!episodeExists)
            {
                response.AddError(Model.Episode, Error.episode_NotFound);
                return response;
            }

            var watchedEpisode = await _userWatchedEpisodeRepository.GetByAsync(x => x.EpisodeId == watchedEpisodeId && x.UserId == userId);

            var result = await _userWatchedEpisodeRepository.Remove(watchedEpisode);

            if (!result)
            {
                response.AddError(Model.WatchedEpisode, Error.watchedEpisode_Deleting);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> MarkSeasonAsNotWatched(int seasonId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var watchedEpisodes = _episodeRepository.GetAllBy(x => x.SeasonId == seasonId);

            List<UserWatchedEpisode> episodesListToRemove = new List<UserWatchedEpisode>();

            foreach (var episode in watchedEpisodes)
            {
                UserWatchedEpisode watchedEpisode = new UserWatchedEpisode();
                watchedEpisode.EpisodeId = episode.Id;
                watchedEpisode.UserId = userId;


                if (await _userWatchedEpisodeRepository.ExistAsync(x =>
                        x.EpisodeId == episode.Id && x.UserId == userId) == true)
                {
                    episodesListToRemove.Add(watchedEpisode);
                }
            }

            foreach (var episode in episodesListToRemove)
            {
                var ep = await _userWatchedEpisodeRepository.GetByAsync(x => x.EpisodeId == episode.EpisodeId);
                var result = await _userWatchedEpisodeRepository.Remove(ep);
                if (!result)
                {
                    response.AddError(Model.Episode, Error.watchedEpisode_Deleting);
                    return response;
                }
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddSeasonToWatched(int seasonId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var watchedEpisodes = _episodeRepository.GetAllBy(x => x.SeasonId == seasonId);
            List<UserWatchedEpisode> userWatchedEpisodesList = new List<UserWatchedEpisode>();

            foreach (var episode in watchedEpisodes)
            {
                UserWatchedEpisode watchedEpisode = new UserWatchedEpisode();
                watchedEpisode.EpisodeId = episode.Id;
                watchedEpisode.UserId = userId;

                
                if (await _userWatchedEpisodeRepository.ExistAsync(x =>
                        x.EpisodeId == episode.Id && x.UserId == userId) == false)
                {
                    userWatchedEpisodesList.Add(watchedEpisode);
                }
            }

            foreach (var episode in userWatchedEpisodesList)
            {
                var result = await _userWatchedEpisodeRepository.AddAsync(episode);
                if (!result)
                {
                    response.AddError(Model.Episode, Error.watchedTvShow_Adding);
                    return response;
                }
            }

            return response;

        }

        public async Task<ResponsesDto<SeasonDto>> GetWeatchedSeasons(int tvSeriesId, string userId)
        {
            var response = new ResponsesDto<SeasonDto>();

            var seasons = _seasonRepository.GetAllBy(x => x.TvShowId == tvSeriesId, x => x.Episodes);
            var mappedSeasons = new List<SeasonDto>();

            foreach (var season in seasons)
            {
                mappedSeasons.Add(_mapper.Map<SeasonDto>(season));
            }

            response.DtoObject = mappedSeasons;

            if (mappedSeasons.Count > 0 && userId != null)
            {
                foreach (var season in mappedSeasons)
                {
                    var seasonWatched = true;
                    foreach (var episode in season.Episodes)
                    {
                        if (await CheckIfEpisodeWatched(episode.Id, userId))
                        {
                            episode.Watched = true;
                        }
                        else
                        {
                            seasonWatched = false;
                        }
                    }

                    season.Watched = seasonWatched;
                }

                response.DtoObject = mappedSeasons;
            }

            return response;
        }

        public async Task<bool> CheckIfEpisodeWatched(int episodeId, string userId)
        {
            var episodeExists =
                await _userWatchedEpisodeRepository.ExistAsync(x => x.EpisodeId == episodeId && x.UserId == userId);

            return episodeExists;
        }
    }
}
