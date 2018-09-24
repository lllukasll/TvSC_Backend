using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.BindingModels.Season;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace TvSC.Services.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly IRepository<Season> _seasonRepository;
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IMapper _mapper;

        public SeasonService(IRepository<Season> seasonRepository, IRepository<TvShow> tvShowRepository, IMapper mapper)
        {
            _seasonRepository = seasonRepository;
            _tvShowRepository = tvShowRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<SeasonResponseDto>> GetAllSeasons(int tvSeriesId)
        {
            var response = new ResponsesDto<SeasonResponseDto>();
            var tvSeriesExists = await _tvShowRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
            }

            var seasons = _seasonRepository.GetAllBy(x => x.TvShowId == tvSeriesId, x => x.Episodes);
            var mappedSeasons = new List<SeasonResponseDto>();
            foreach (var season in seasons)
            {
                mappedSeasons.Add(_mapper.Map<SeasonResponseDto>(season));
            }

            response.DtoObject = mappedSeasons;

            return response;
        }

        public async Task<ResponseDto<SeasonResponseDto>> GetSeason(int seasonId)
        {
            var response = new ResponseDto<SeasonResponseDto>();
            var seasonExists = await _seasonRepository.ExistAsync(x => x.Id == seasonId);
            if (!seasonExists)
            {
                response.AddError(Model.Season, Error.season_NotFound);
            }

            var season = await _seasonRepository.GetByAsync(x => x.Id == seasonId);
            var mappedSeason = _mapper.Map<SeasonResponseDto>(season);

            response.DtoObject = mappedSeason;
            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddSeason(int tvSeriesId, AddSeasonBindingModel seasonBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            var tvSeriesExists = await _tvShowRepository.ExistAsync(x => x.Id == tvSeriesId);

            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var seasonExists =
                await _seasonRepository.GetByAsync(x => x.SeasonNumber == seasonBindingModel.SeasonNumber && x.TvShowId == tvSeriesId);

            if (seasonExists != null)
            {
                response.AddError(Model.Season, Error.season_Exists);
                return response;
            }

            var season = _mapper.Map<Season>(seasonBindingModel);
            season.TvShowId = tvSeriesId;

            var result = await _seasonRepository.AddAsync(season);
            if (!result)
            {
                response.AddError(Model.Season, Error.season_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> UpdateSeason(int seasonId, UpdateSeasonBindingModel seasonBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            var seasonExists = await _seasonRepository.ExistAsync(x => x.Id == seasonId);
            if (!seasonExists)
            {
                response.AddError(Model.Season, Error.season_NotFound);
            }

            var season = await _seasonRepository.GetByAsync(x => x.Id == seasonId);
            season.SeasonNumber = seasonBindingModel.SeasonNumber;

            var result = await _seasonRepository.UpdateAsync(season);
            if (!result)
            {
                response.AddError(Model.Season, Error.season_Updating);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteSeason(int seasonId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var seasonExists = await _seasonRepository.ExistAsync(x => x.Id == seasonId);
            if (!seasonExists)
            {
                response.AddError(Model.Season, Error.season_NotFound);
            }

            var season = await _seasonRepository.GetByAsync(x => x.Id == seasonId);

            var result = await _seasonRepository.Remove(season);
            if (!result)
            {
                response.AddError(Model.Season, Error.season_Deleting);
                return response;
            }

            return response;
        }
    }
}
