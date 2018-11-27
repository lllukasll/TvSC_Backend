using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Episode;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Episodes;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IRepository<Season> _seasonRepository;
        private readonly IRepository<TvShow> _tvSeriesRepository;
        private readonly IMapper _mapper;

        public EpisodeService(IRepository<Episode> episodeRepository, IRepository<Season> seasonRepository, IRepository<TvShow> tvSeriesRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository;
            _seasonRepository = seasonRepository;
            _tvSeriesRepository = tvSeriesRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<EpisodeDto>> GetEpisodes(int seasonId)
        {
            var response = new ResponsesDto<EpisodeDto>();
            var seasonExists = await _seasonRepository.ExistAsync(x => x.Id == seasonId);
            if (!seasonExists)
            {
                response.AddError(Model.Season, Error.season_NotFound);
                return response;
            }

            var episodes = _episodeRepository.GetAllBy(x => x.SeasonId == seasonId);
            var mappedEpisodes = new List<EpisodeDto>();

            foreach (var episode in episodes)
            {
                mappedEpisodes.Add(_mapper.Map<EpisodeDto>(episode));
            }

            response.DtoObject = mappedEpisodes;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddEpisode(int seasonId, AddEpisodeBindingModel episodeBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            var seasonExists = await _seasonRepository.ExistAsync(x => x.Id == seasonId);
            if (!seasonExists)
            {
                response.AddError(Model.Season, Error.season_NotFound);
                return response;
            }

            var episodeInDb = await _episodeRepository.GetByAsync(x => x.EpisodeNumber == episodeBindingModel.EpisodeNumber && x.SeasonId == seasonId);

            if (episodeInDb != null)
            {
                response.AddError(Model.Episode, Error.episode_Exists);
                return response;
            }

            var episode = _mapper.Map<Episode>(episodeBindingModel);
            episode.SeasonId = seasonId;

            var result = await _episodeRepository.AddAsync(episode);
            if (!result)
            {
                response.AddError(Model.Episode, Error.episode_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> UpdateEpisode(int episodeId, UpdateEpisodeBindingModel episodeBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            var episodeExists = await _episodeRepository.ExistAsync(x => x.Id == episodeId);
            if (!episodeExists)
            {
                response.AddError(Model.Episode, Error.episode_NotFound);
                return response;
            }

            var episode = _mapper.Map<Episode>(episodeBindingModel);
            episode.Id = episodeId;

            var result = await _episodeRepository.UpdateAsync(episode);
            if (!result)
            {
                response.AddError(Model.Episode, Error.episode_Updating);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteEpisode(int episodeId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var episodeExists = await _episodeRepository.ExistAsync(x => x.Id == episodeId);
            if (!episodeExists)
            {
                response.AddError(Model.Episode, Error.episode_NotFound);
                return response;
            }

            var episode = await _episodeRepository.GetByAsync(x => x.Id == episodeId);

            var result = await _episodeRepository.Remove(episode);
            if (!result)
            {
                response.AddError(Model.Episode, Error.episode_Deleting);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<EpisodeDto>> GetClosestEpisode(int tvSeriesId)
        {
            var response = new ResponseDto<EpisodeDto>();
            var tvSeriesExists = await _tvSeriesRepository.ExistAsync(x => x.Id == tvSeriesId);
            if (!tvSeriesExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            DateTime localDate = DateTime.Now;

            var episodes = _episodeRepository.GetAllBy(x => x.Season.TvShow.Id == tvSeriesId && x.AiringDate >= localDate);

            Episode episode = new Episode();

            if (episodes.Any())
            {
                episode = episodes.OrderBy(x => x.AiringDate).First();
                response.DtoObject = _mapper.Map<EpisodeDto>(episode);
            }

            return response;
        }
    }
}
