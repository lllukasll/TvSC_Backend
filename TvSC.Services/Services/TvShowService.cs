using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
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
        private readonly ITvShowCategoriesAssignmentsService _tvShowCategoriesAssignmentsService;
        private readonly IMapper _mapper;
        public TvShowService(IRepository<TvShow> tvShowRepository, IActorAssignmentService actorAssignmentService, ITvShowCategoriesAssignmentsService tvShowCategoriesAssignmentsService, IMapper mapper)
        {
            _tvShowRepository = tvShowRepository;
            _actorAssignmentService = actorAssignmentService;
            _tvShowCategoriesAssignmentsService = tvShowCategoriesAssignmentsService;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<TvShowResponse>> GetAllTvShows()
        {
            var response = new ResponsesDto<TvShowResponse>();

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

            response.DtoObject = mappedTvShows;

            return response;
        }

        public async Task<ResponseDto<TvShowResponse>> GetTvSeries(int tvSeriesId)
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

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await tvShowBindingModel.Photo.CopyToAsync(stream);
            }

            var tvShow = _mapper.Map<TvShow>(tvShowBindingModel);
            tvShow.PhotoName = fileName;

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

            var tvSeries = _mapper.Map<TvShow>(tvShowBindingModel);
            tvSeries.Id = id;

            var result = await _tvShowRepository.UpdateAsync(tvSeries);
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
