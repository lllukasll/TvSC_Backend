using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Rating;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Repo.Repositories;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<TvSeriesUserRating> _tvSeriesUserRatingRepository;
        private readonly IRepository<TvSeriesRatings> _tvSeriesRatingsRepository;
        private readonly IRepository<TvShow> _tvSeriesRepository;
        private readonly IMapper _mapper;

        public RatingService(IRepository<TvSeriesUserRating> tvSeriesUserRatingRepository,IRepository<TvSeriesRatings> tvSeriesRatingsRepository, IRepository<TvShow> tvSeriesRepository, IMapper mapper)
        {
            _tvSeriesUserRatingRepository = tvSeriesUserRatingRepository;
            _tvSeriesRatingsRepository = tvSeriesRatingsRepository;
            _tvSeriesRepository = tvSeriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<GetTvSeriesRatingDto>> GetTvSeriesRating(int tvSeriesId)
        {
            var response = new ResponseDto<GetTvSeriesRatingDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvSeriesRatings = _tvSeriesUserRatingRepository.GetAllBy(x => x.TvShowId == tvSeriesId);
            GetTvSeriesRatingDto tmpRating = new GetTvSeriesRatingDto();

            foreach (var tvSeriesRating in tvSeriesRatings)
            {
                tmpRating.Story += tvSeriesRating.Story;
                tmpRating.Music += tvSeriesRating.Music;
                tmpRating.Effects += tvSeriesRating.Effects;
                tmpRating.Average += tvSeriesRating.Average;
            }

            tmpRating.Story = tmpRating.Story / tvSeriesRatings.Count();
            tmpRating.Music = tmpRating.Music / tvSeriesRatings.Count();
            tmpRating.Effects = tmpRating.Effects / tvSeriesRatings.Count();
            tmpRating.Average = tmpRating.Average / tvSeriesRatings.Count();

            response.DtoObject = tmpRating;

            return response;
        }

        public async Task<ResponseDto<GetTvSeriesRatingDto>> GetTvSeriesRatingForUser(string loggedUser, int tvSeriesId)
        {
            var response = new ResponseDto<GetTvSeriesRatingDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var rating = await _tvSeriesUserRatingRepository.GetByAsync(x => x.UserId == loggedUser);
            if (rating == null)
            {
                response.AddError(Model.Rating, Error.rating_Not_Added);
                return response;
            }

            var mappedRating = _mapper.Map<GetTvSeriesRatingDto>(rating);

            response.DtoObject = mappedRating;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddTvSeriesUserRating(string userLogged, int tvSeriesId,
            AddTvSeriesRatingBindingModel tvSeriesRatingBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var ratingExists = await _tvSeriesUserRatingRepository.ExistAsync(x => x.UserId == userLogged);
            if (ratingExists)
            {
                response.AddError(Model.Rating, Error.rating_Already_Added);
                return response;
            }

            var userRating = _mapper.Map<TvSeriesUserRating>(tvSeriesRatingBindingModel);
            userRating.Average = (userRating.Effects + userRating.Music + userRating.Story ) / 3;
            userRating.TvShowId = tvSeriesId;
            userRating.UserId = userLogged;

            var result = await _tvSeriesUserRatingRepository.AddAsync(userRating);

            if (!result)
            {
                response.AddError(Model.Rating, Error.rating_Adding);
                return response;
            }

            response = await AddTvSeriesRating(userRating, response);

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> UpdateTvSeriesUserRating(string userLogged, int ratingId,
            UpdateTvSeriesRatingBindingModel tvSeriesRatingBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();

            var ratingExists = await _tvSeriesUserRatingRepository.ExistAsync(x => x.Id == ratingId);
            if (!ratingExists)
            {
                response.AddError(Model.Rating, Error.rating_NotFound);
                return response;
            }

            var rating = await _tvSeriesUserRatingRepository.GetByAsync(x => x.Id == ratingId);

            if (rating.UserId != userLogged)
            {
                response.AddError(Model.Rating, Error.rating_User_Not_Assigned);
                return response;
            }

            rating.Story = tvSeriesRatingBindingModel.Story;
            rating.Effects = tvSeriesRatingBindingModel.Effects;
            rating.Music = tvSeriesRatingBindingModel.Music;
            rating.Average = (tvSeriesRatingBindingModel.Story + tvSeriesRatingBindingModel.Effects + tvSeriesRatingBindingModel.Music ) / 3;

            var result = await _tvSeriesUserRatingRepository.UpdateAsync(rating);

            if (!result)
            {
                response.AddError(Model.Rating, Error.rating_Updating);
                return response;
            }

            response = await AddTvSeriesRating(rating, response);

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteTvSeriesUserRating(int ratingId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var ratingExists = await _tvSeriesUserRatingRepository.ExistAsync(x => x.Id == ratingId);
            if (!ratingExists)
            {
                response.AddError(Model.Rating, Error.rating_NotFound);
                return response;
            }

            var rating = await _tvSeriesUserRatingRepository.GetByAsync(x => x.Id == ratingId);

            var result = await _tvSeriesUserRatingRepository.Remove(rating);

            if (!result)
            {
                response.AddError(Model.Rating, Error.rating_Deleting);
                return response;
            }

            response = await AddTvSeriesRating(rating, response);

            return response;
        }

        private async Task<ResponseDto<BaseModelDto>> AddTvSeriesRating(TvSeriesUserRating rating, ResponseDto<BaseModelDto> response)
        {
            var tvSeriesUserRatings = _tvSeriesUserRatingRepository.GetAllBy(x => x.TvShowId == rating.TvShowId);

            TvSeriesRatings ratings = new TvSeriesRatings();

            foreach (var tvSeriesUserRating in tvSeriesUserRatings)
            {
                ratings.Effects += tvSeriesUserRating.Effects;
                ratings.Music += tvSeriesUserRating.Music;
                ratings.Story += tvSeriesUserRating.Average;
                ratings.Average += tvSeriesUserRating.Average;
            }

            ratings.Effects = ratings.Effects / tvSeriesUserRatings.Count();
            ratings.Music = ratings.Music / tvSeriesUserRatings.Count();
            ratings.Story = ratings.Story / tvSeriesUserRatings.Count();
            ratings.Average = ratings.Average / tvSeriesUserRatings.Count();
            ratings.TvShowId = rating.TvShowId;

            var tvSeriesRatingExists = await _tvSeriesRatingsRepository.ExistAsync(x => x.TvShowId == rating.TvShowId);

            if (tvSeriesRatingExists)
            {
                var tvSeriesRatings = await _tvSeriesRatingsRepository.GetByAsync(x => x.TvShowId == rating.TvShowId);
                tvSeriesRatings.Average = ratings.Average;
                tvSeriesRatings.Effects = ratings.Effects;
                tvSeriesRatings.Music = ratings.Music;
                tvSeriesRatings.Story = ratings.Story;

                var updateResult = await _tvSeriesRatingsRepository.UpdateAsync(tvSeriesRatings);

                if (!updateResult)
                {
                    response.AddError(Model.Rating, Error.rating_Updating);
                    return response;
                }
            }
            else
            {
                var addingResult = await _tvSeriesRatingsRepository.AddAsync(ratings);

                if (!addingResult)
                {
                    response.AddError(Model.Rating, Error.rating_Adding);
                    return response;
                }
            }

            return response;
        }
    }
}
