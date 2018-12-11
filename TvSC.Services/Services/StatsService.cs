using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class StatsService : IStateService
    {
        private readonly IRepository<UserWatchedEpisode> _userWatchedEpisodeRepository;
        private readonly IRepository<TvSeriesUserRating> _tvSeriesUserRatingRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<UserFavouriteTvShows> _userFavouriteTvShowsRepository;

        public StatsService(IRepository<UserWatchedEpisode> userWatchedEpisodeRepository, IRepository<TvSeriesUserRating> tvSeriesUserRatingRepository, IRepository<Comment> commentRepository, IRepository<UserFavouriteTvShows> userFavouriteTvShowsRepository)
        {
            _userWatchedEpisodeRepository = userWatchedEpisodeRepository;
            _tvSeriesUserRatingRepository = tvSeriesUserRatingRepository;
            _commentRepository = commentRepository;
            _userFavouriteTvShowsRepository = userFavouriteTvShowsRepository;
        }

        public async Task<ResponseDto<UserStatsResponseDto>> GetUserStats(string userId)
        {
            var response = new ResponseDto<UserStatsResponseDto>();

            var episodesWatched = _userWatchedEpisodeRepository.GetAllBy(x => x.UserId == userId, x => x.Episode.Season.TvShow);
            int hoursCount = 0;
            int episodesCount = 0;

            var userTvSeriesRating = _tvSeriesUserRatingRepository.GetAllBy(x => x.UserId == userId);
            int averageRating = 0;

            foreach (var rating in userTvSeriesRating)
            {
                averageRating += rating.Average;
            }

            if (userTvSeriesRating.Any())
            {
                averageRating = averageRating / userTvSeriesRating.Count();
            }
            
            int ratedCount = userTvSeriesRating.Count();

            var userComments = _commentRepository.GetAllBy(x => x.UserId == userId);
            int commentsCount = userComments.Count();

            var userFavouriteTvShows = _userFavouriteTvShowsRepository.GetAllBy(x => x.UserId == userId);
            int favouriteCount = userFavouriteTvShows.Count();

            foreach (var episode in episodesWatched)
            {
                hoursCount += episode.Episode.Season.TvShow.EpisodeLength;
                episodesCount += 1;
            }

            UserStatsResponseDto stats = new UserStatsResponseDto();
            stats.EpisodesWatched = episodesCount;
            stats.HoursWatched = hoursCount / 60;
            stats.AverageRating = averageRating;
            stats.RatedCount = ratedCount;
            stats.CommentsCount = commentsCount;
            stats.LikedTvSeries = favouriteCount;

            response.DtoObject = stats;

            return response;
        }
    }
}
