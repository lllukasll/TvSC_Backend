using System.Threading.Tasks;
using TvSC.Data.DtoModels;

namespace TvSC.Services.Interfaces
{
    public interface IStateService
    {
        Task<ResponseDto<UserStatsResponseDto>> GetUserStats(string userId);
    }
}