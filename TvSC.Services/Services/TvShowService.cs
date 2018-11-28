using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IActorAssignmentService _actorAssignmentService;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ITvShowCategoriesAssignmentsService _tvShowCategoriesAssignmentsService;
        private readonly IUserFavouriteTvShowsService _userFavouriteTvShowsService;
        private readonly IRatingService _ratingService;
        private readonly IUserWatchedEpisodeService _userWatchedEpisodeService;
        private readonly IMapper _mapper;
        public TvShowService(IRepository<TvShow> tvShowRepository, IActorAssignmentService actorAssignmentService, IRepository<Category> categoryRepository,
                             ITvShowCategoriesAssignmentsService tvShowCategoriesAssignmentsService, IUserFavouriteTvShowsService userFavouriteTvShowsService ,
                             IRatingService ratingService, IUserWatchedEpisodeService userWatchedEpisodeService, IMapper mapper)
        {
            _tvShowRepository = tvShowRepository;
            _actorAssignmentService = actorAssignmentService;
            _categoryRepository = categoryRepository;
            _tvShowCategoriesAssignmentsService = tvShowCategoriesAssignmentsService;
            _userFavouriteTvShowsService = userFavouriteTvShowsService;
            _ratingService = ratingService;
            _userWatchedEpisodeService = userWatchedEpisodeService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<SortedTvShowsResponse>> GetAllTvShows(SearchByParameters parameters)
        {
            var response = new ResponseDto<SortedTvShowsResponse>();
            response.DtoObject = new SortedTvShowsResponse();
            response.DtoObject.TvShowsList = new List<TvShowResponse>();

            var tvShows = await _tvShowRepository.GetAll(x => x.TvSeriesRatings, x => x.Seasons).ToListAsync();
            var mappedTvShows = new List<TvShowResponse>();

            foreach (var tvShow in tvShows)
            {
                var assignments = await _actorAssignmentService.GetTvShowAssignments(tvShow.Id);
                var mappedTvShow = _mapper.Map<TvShowResponse>(tvShow);

                var categoryAssignments = await _tvShowCategoriesAssignmentsService.GetTvShowsCategories(tvShow.Id);

                mappedTvShow.Actors = assignments.DtoObject;
                mappedTvShow.Categories = categoryAssignments.DtoObject;
                mappedTvShows.Add(mappedTvShow);
            }

            if ((parameters.Categories != null && parameters.Categories.Length > 0) ||
                (parameters.Networks != null && parameters.Networks.Length > 0))
            {
                var finalTvShows = FilterByCategories(parameters.Categories, mappedTvShows);
                response.DtoObject.TvShowsList = finalTvShows;

                
                if (parameters.Networks != null && parameters.Networks.Length > 0)
                {
                    response.DtoObject.TvShowsList = FilterByNetworks(parameters, finalTvShows, mappedTvShows);
                }
            }
            else
            {
                response.DtoObject.TvShowsList = mappedTvShows;
            }

            if (parameters.Status != 0)
            {
                response.DtoObject.TvShowsList =
                    response.DtoObject.TvShowsList.FindAll(x => x.Status == parameters.Status);
            }

            var totalTvShowsNumber = response.DtoObject.TvShowsList.Count;
            double totalPageCount = Math.Ceiling((double)totalTvShowsNumber / parameters.PageSize);

            response.DtoObject.TvShowsList = response.DtoObject.TvShowsList.Skip(parameters.PageNumber * parameters.PageSize - parameters.PageSize)
                .Take(parameters.PageSize).ToList();

            response.DtoObject.PageSize = parameters.PageSize;
            response.DtoObject.ActivePageNumber = parameters.PageNumber;
            response.DtoObject.TotalPageNumber = totalPageCount;

            return response;
        }

        private List<TvShowResponse> FilterByCategories(string[] Categories, List<TvShowResponse> mappedTvShows)
        {
            var finalTvShows = new List<TvShowResponse>();
            if (Categories != null && Categories.Length > 0)
            {
                foreach (var category in Categories)
                {
                    var tvShowsByCategory = mappedTvShows.FindAll(x => x.Categories.Any(y => y.Name == category));

                    foreach (var tvShow in tvShowsByCategory)
                    {
                        if (!finalTvShows.Contains(tvShow))
                        {
                            finalTvShows.Add(tvShow);
                        }
                    }
                }
            }

            return finalTvShows;
        }

        private List<TvShowResponse> FilterByNetworks(SearchByParameters parameters, List<TvShowResponse> finalTvShows, List<TvShowResponse> mappedTvShows)
        {
            var tvShowsByNetworkFinal = new List<TvShowResponse>();

            foreach (var network in parameters.Networks)
            {
                if (parameters.Categories != null && parameters.Categories.Length > 0)
                {
                    var tvShowsByNetwork = finalTvShows.FindAll(x => x.Network == network);

                    foreach (var tvShow in tvShowsByNetwork)
                    {
                        if (!tvShowsByNetworkFinal.Contains(tvShow))
                        {
                            tvShowsByNetworkFinal.Add(tvShow);
                        }
                    }
                }
                else
                {
                    var tvShowsByNetwork = mappedTvShows.FindAll(x => x.Network == network);

                    foreach (var tvShow in tvShowsByNetwork)
                    {
                        if (!tvShowsByNetworkFinal.Contains(tvShow))
                        {
                            tvShowsByNetworkFinal.Add(tvShow);
                        }
                    }
                }
            }

            return tvShowsByNetworkFinal;
        }

        public async Task<ResponseDto<TvShowResponse>> GetTvSeries(int tvSeriesId, string userId)
        {
            var response = new ResponseDto<TvShowResponse>();
            var tvSeriesExists = await _tvShowRepository.ExistAsync(x => x.Id == tvSeriesId);

            if(!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvSeries = await _tvShowRepository.GetByAsync(x => x.Id == tvSeriesId, x => x.Seasons, x => x.TvSeriesRatings);
            await _tvShowRepository.LoadRelatedCollection(tvSeries, x => x.Seasons, x => x.Episodes);
            var mappedTvSeries = _mapper.Map<TvShowResponse>(tvSeries);

            var assignments = await _actorAssignmentService.GetTvShowAssignments(tvSeriesId);
            var categoryAssignments = await _tvShowCategoriesAssignmentsService.GetTvShowsCategories(tvSeriesId);

            mappedTvSeries.Actors = assignments.DtoObject;
            mappedTvSeries.Categories = categoryAssignments.DtoObject;

            if (userId != null)
            {
                var userFavourites = await _userFavouriteTvShowsService.GetUserFavouriteTvSeries(userId);
                mappedTvSeries.IsFavourite = userFavourites.DtoObject.Any(x => x.Id == tvSeriesId);

                var userRating = await _ratingService.GetTvSeriesRatingForUser(userId, tvSeriesId);
                if (!userRating.ErrorOccurred)
                {
                    mappedTvSeries.UserRatingDto = new TvSeriesRatingsDto
                    {
                        Id = userRating.DtoObject.Id,
                        Average = userRating.DtoObject.Average,
                        Music = userRating.DtoObject.Music,
                        Effects = userRating.DtoObject.Effects,
                        Story = userRating.DtoObject.Story
                    };
                }
            }

            response.DtoObject = mappedTvSeries;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddTvShow(AddTvShowBindingModel tvShowBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            if (!Directory.Exists("wwwroot\\TvShowsPictures"))
                Directory.CreateDirectory("wwwroot\\TvShowsPictures");

            var fileName = Guid.NewGuid() + Path.GetExtension(tvShowBindingModel.Photo.FileName);
            var filePath = Path.Combine("wwwroot\\TvShowsPictures", fileName);

            var backgroundFileName = Guid.NewGuid() + Path.GetExtension(tvShowBindingModel.BackgroundPhoto.FileName);
            var backgroundFilePath = Path.Combine("wwwroot\\TvShowsPictures", backgroundFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await tvShowBindingModel.Photo.CopyToAsync(stream);
            }

            using (var stream = new FileStream(backgroundFilePath, FileMode.Create))
            {
                await tvShowBindingModel.Photo.CopyToAsync(stream);
            }

            var tvShow = _mapper.Map<TvShow>(tvShowBindingModel);
            tvShow.PhotoName = fileName;
            tvShow.BackgroundPhotoName = backgroundFileName;

            var tvShowInDatabase = await _tvShowRepository.GetByAsync(x => x.Name.ToLower() == tvShowBindingModel.Name.ToLower());
            if (tvShowInDatabase != null)
            {
                response.AddError(Model.TvShow, Error.tvShow_Exists);
                return response;
            }

            var result = await _tvShowRepository.AddAsync(tvShow);
            if (!result)
            {
                response.AddError(Model.TvShow, Error.tvShow_Adding);
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> UpdateTvShow(int id, UpdateTvShowBindingModel tvShowBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            var tvSeriesExists = await _tvShowRepository.ExistAsync(x => x.Id == id);

            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvSeriesFromDb = await _tvShowRepository.GetByAsync(x => x.Id == id, x => x.Categories, x => x.Seasons, x => x.TvSeriesRatings, x => x.TvSeriesUserRatings, x => x.UserFavouriteTvShows);
            if (tvSeriesFromDb.PhotoName != null)
            {
                if (File.Exists(("wwwroot\\TvShowsPictures\\" + tvSeriesFromDb.PhotoName)))
                {
                    File.Delete("wwwroot\\TvShowsPictures\\" + tvSeriesFromDb.PhotoName);
                }
            }

            if (tvSeriesFromDb.BackgroundPhotoName != null)
            {
                if (File.Exists(("wwwroot\\TvShowsPictures\\" + tvSeriesFromDb.BackgroundPhotoName)))
                {
                    File.Delete("wwwroot\\TvShowsPictures\\" + tvSeriesFromDb.BackgroundPhotoName);
                }
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(tvShowBindingModel.Photo.FileName);
            var filePath = Path.Combine("wwwroot\\TvShowsPictures", fileName);

            var backgroundFileName = Guid.NewGuid() + Path.GetExtension(tvShowBindingModel.BackgroundPhoto.FileName);
            var backgroundFilePath = Path.Combine("wwwroot\\TvShowsPictures", backgroundFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await tvShowBindingModel.Photo.CopyToAsync(stream);
            }

            using (var stream = new FileStream(backgroundFilePath, FileMode.Create))
            {
                await tvShowBindingModel.BackgroundPhoto.CopyToAsync(stream);
            }

            tvSeriesFromDb.Name = tvShowBindingModel.Name;
            tvSeriesFromDb.Description = tvShowBindingModel.Description;
            tvSeriesFromDb.LongDescription = tvShowBindingModel.LongDescription;
            tvSeriesFromDb.Network = tvShowBindingModel.Network;
            tvSeriesFromDb.EmissionHour = tvShowBindingModel.EmissionHour;
            tvSeriesFromDb.EpisodeLength = tvShowBindingModel.EpisodeLength;
            tvSeriesFromDb.PhotoName = fileName;
            tvSeriesFromDb.BackgroundPhotoName = backgroundFileName;

            var result = await _tvShowRepository.UpdateAsync(tvSeriesFromDb);
            if (!result)
            {
                response.AddError(Model.TvShow, Error.tvShow_Updating);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteTvShow(int tvSeriesId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var tvSeriesExists = await _tvShowRepository.ExistAsync(x => x.Id == tvSeriesId);

            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvSeries = await _tvShowRepository.GetByAsync(x => x.Id == tvSeriesId);
            var result = await _tvShowRepository.Remove(tvSeries);

            if (!result)
            {
                response.AddError(Model.TvShow, Error.tvShow_Deleting);
                return response;
            }

            return response;
        }

    }
}
