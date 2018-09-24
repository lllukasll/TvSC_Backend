using System.Threading.Tasks;
using TvSC.Data.BindingModels.Episode;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Episodes;

namespace TvSC.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<ResponsesDto<EpisodeDto>> GetEpisodes(int seasonId);
        Task<ResponseDto<BaseModelDto>> AddEpisode(int seasonId, AddEpisodeBindingModel episodeBindingModel);
        Task<ResponseDto<BaseModelDto>> UpdateEpisode(int episodeId, UpdateEpisodeBindingModel episodeBindingModel);
        Task<ResponseDto<BaseModelDto>> DeleteEpisode(int episodeId);
    }
}