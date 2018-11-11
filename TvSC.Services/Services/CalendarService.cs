using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Episodes;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;
using IMapper = AutoMapper.IMapper;

namespace TvSC.Services.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IMapper _mapper;

        public CalendarService(IRepository<Episode> episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<ReturnEpisodeDto>> GetMonthEpisodes(int monthNumber)
        {
            var response = new ResponsesDto<ReturnEpisodeDto>();
            if (monthNumber > 12 || monthNumber < 1)
            {
                response.AddError(Model.Calendar, Error.calendar_Wrong_Month);
                return response;
            }

            var episodes = _episodeRepository.GetAllBy(x => x.AiringDate.Month == monthNumber, x => x.Season, x => x.Season.TvShow, x => x.Season.TvShow.TvSeriesRatings);
            
            var mappedEpisodes = new List<ReturnEpisodeDto>();

            foreach (var episode in episodes)
            {
                mappedEpisodes.Add(_mapper.Map<ReturnEpisodeDto>(episode));
            }

            response.DtoObject = mappedEpisodes;

            return response;
        }

        public async Task<ResponsesDto<ReturnEpisodeDto>> GetWeekEpisodes(DateTime date)
        {
            var response = new ResponsesDto<ReturnEpisodeDto>();
            var day = date.DayOfWeek;

            return response;
        }
    }
}
