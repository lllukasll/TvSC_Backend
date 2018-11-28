using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Actor;
using TvSC.Data.DtoModels.Assignments;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Services.Interfaces
{
    public interface IActorAssignmentService
    {
        Task<ResponseDto<BaseModelDto>> AssignActorToTvShow(int actorId, int tvShowId, string characterName);
        Task<ResponseDto<BaseModelDto>> RemoveActorAssignment(int assignmentId);
        Task<ResponsesDto<TvSeriesAssignmensResponseDto>> GetTvShowAssignments(int tvShowId);
        Task<ResponsesDto<ActorAssignmentsResponseDto>> GetActorAssignments(int actorId);
    }
}