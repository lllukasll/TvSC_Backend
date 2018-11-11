using System.Threading.Tasks;
using TvSC.Data.BindingModels.Actor;
using TvSC.Data.BindingModels.Episode;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Actor;

namespace TvSC.Services.Interfaces
{
    public interface IActorService
    {
        Task<ResponseDto<BaseModelDto>> AddActor(AddActorBindingModel actorBindingModel);
        Task<ResponseDto<BaseModelDto>> DeleteActor(int actorId);
        Task<ResponsesDto<ActorDto>> GetActors();
        Task<ResponseDto<ActorDto>> GetActor(int actorId);
        //Task<ResponseDto<BaseModelDto>> RemoveActorAssignment(int actorId, int tvShowId);
    }
}