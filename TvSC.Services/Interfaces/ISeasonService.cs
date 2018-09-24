using System.Threading.Tasks;
using TvSC.Data.BindingModels.Season;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Season;

namespace TvSC.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<ResponsesDto<SeasonResponseDto>> GetAllSeasons(int tvSeriesId);
        Task<ResponseDto<BaseModelDto>> AddSeason(int tvSeriesId, AddSeasonBindingModel seasonBindingModel);
        Task<ResponseDto<BaseModelDto>> UpdateSeason(int seasonId, UpdateSeasonBindingModel seasonBindingModel);
        Task<ResponseDto<BaseModelDto>> DeleteSeason(int seasonId);
        Task<ResponseDto<SeasonResponseDto>> GetSeason(int seasonId);
    }
}