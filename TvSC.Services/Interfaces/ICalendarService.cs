using System;
using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Episodes;

namespace TvSC.Services.Interfaces
{
    public interface ICalendarService
    {
        Task<ResponsesDto<ReturnEpisodeDto>> GetMonthEpisodes(int monthNumber);
        Task<ResponsesDto<ReturnEpisodeDto>> GetWeekEpisodes(DateTime date);
    }
}