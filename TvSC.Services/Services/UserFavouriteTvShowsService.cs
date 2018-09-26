using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.FavouriteTvSeries;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class UserFavouriteTvShowsService : IUserFavouriteTvShowsService
    {
        private readonly IRepository<UserFavouriteTvShows> _userFavouriteTvShowsRepository;
        private readonly IRepository<TvShow> _tvSeriesRepository;
        private readonly IMapper _mapper;

        public UserFavouriteTvShowsService(IRepository<UserFavouriteTvShows> userFavouriteTvShowsRepository, IRepository<TvShow> tvSeriesRepository, IMapper mapper)
        {
            _userFavouriteTvShowsRepository = userFavouriteTvShowsRepository;
            _tvSeriesRepository = tvSeriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<FavouriteTvSeriesResponseDto>> GetUserFavouriteTvSeries(string userId)
        {
            var response = new ResponsesDto<FavouriteTvSeriesResponseDto>();

            var favouriteTvSeriesList = _userFavouriteTvShowsRepository.GetAllBy(x => x.UserId == userId, x => x.TvShow);            
            var mappedFavouriteTvSeries = new List<FavouriteTvSeriesResponseDto>();

            foreach (var favouriteTvSeries in favouriteTvSeriesList)
            {
                mappedFavouriteTvSeries.Add(_mapper.Map<FavouriteTvSeriesResponseDto>(favouriteTvSeries));
            }

            response.DtoObject = mappedFavouriteTvSeries;
            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int tvSeriesId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var favouriteTvSeriesInDb = await _userFavouriteTvShowsRepository.GetByAsync(x => x.TvShowId == tvSeriesId && x.UserId == userId);
            if (favouriteTvSeriesInDb != null)
            {
                response.AddError(Model.FavouriteTvShow, Error.favouriteTvShow_Already_Exists);
                return response;
            }

            UserFavouriteTvShows favouriteTvShow = new UserFavouriteTvShows();
            favouriteTvShow.TvShowId = tvSeriesId;
            favouriteTvShow.UserId = userId;

            var result = await _userFavouriteTvShowsRepository.AddAsync(favouriteTvShow);

            if (!result)
            {
                response.AddError(Model.FavouriteTvShow, Error.favouriteTvShow_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteUserFavouriteTvSeries(int favouriteTvSeriesId, string userId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var favouriteTvSeriesExists = await _userFavouriteTvShowsRepository.ExistAsync(x => x.Id == favouriteTvSeriesId);
            if (!favouriteTvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var favouriteTvSeries = await _tvSeriesRepository.GetByAsync(x => x.Id == favouriteTvSeriesId);

            var result = await _tvSeriesRepository.Remove(favouriteTvSeries);

            if (!result)
            {
                response.AddError(Model.FavouriteTvShow, Error.favouriteTvShow_Deleting);
                return response;
            }

            return response;
        }
    }
}
