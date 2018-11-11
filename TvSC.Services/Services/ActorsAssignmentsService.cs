using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Actor;
using TvSC.Data.DtoModels.Assignments;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class ActorsAssignmentsService : IActorAssignmentService
    {
        private readonly IRepository<ActorsAssignments> _actorsAssignmentsRepository;
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IMapper _mapper;

        public ActorsAssignmentsService(IRepository<ActorsAssignments> actorsAssignmentsRepository, IRepository<TvShow> tvShowRepository, IRepository<Actor> actorRepository, IMapper mapper)
        {
            _actorsAssignmentsRepository = actorsAssignmentsRepository;
            _tvShowRepository = tvShowRepository;
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<ActorDto>> GetTvShowAssignments(int tvShowId)
        {
            var response = new ResponsesDto<ActorDto>();

            var tvShowExists = await _tvShowRepository.ExistAsync(x => x.Id == tvShowId);
            if (!tvShowExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var assignments = _actorsAssignmentsRepository.GetAllBy(x => x.TvShow.Id == tvShowId, x => x.Actor);
            var assignmentsMapped = new List<ActorDto>();

            foreach (var assignment in assignments)
            {
                var actor = await _actorRepository.GetByAsync(x => x.Id == assignment.Actor.Id);
                var actorMapped = _mapper.Map<ActorDto>(actor);
                assignmentsMapped.Add(actorMapped);
            }

            response.DtoObject = assignmentsMapped;

            return response;
        }

        public async Task<ResponsesDto<TvShowResponse>> GetActorAssignments(int actorId)
        {
            var response = new ResponsesDto<TvShowResponse>();

            var actorExists = await _actorRepository.ExistAsync(x => x.Id == actorId);
            if (!actorExists)
            {
                response.AddError(Model.Actor, Error.actor_NotExists);
                return response;
            }

            var assignments = _actorsAssignmentsRepository.GetAllBy(x => x.Actor.Id == actorId, x => x.TvShow);
            var assignmentsMapped = new List<TvShowResponse>();

            foreach (var assignment in assignments)
            {
                var tvShow = await _tvShowRepository.GetByAsync(x => x.Id == assignment.TvShow.Id);
                var tvShowMapped = _mapper.Map<TvShowResponse>(tvShow);
                assignmentsMapped.Add(tvShowMapped);
            }

            response.DtoObject = assignmentsMapped;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AssignActorToTvShow(int actorId, int tvShowId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var actorExists = await _actorRepository.ExistAsync(x => x.Id == actorId);
            if (!actorExists)
            {
                response.AddError(Model.Actor, Error.actor_NotExists);
                return response;
            }

            var tvShowExists = await _tvShowRepository.ExistAsync(x => x.Id == tvShowId);
            if (!tvShowExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvShow = await _tvShowRepository.GetByAsync(x => x.Id == tvShowId);
            var actor = await _actorRepository.GetByAsync(x => x.Id == actorId);

            var assingmentExists =
                await _actorsAssignmentsRepository.ExistAsync(x => x.Actor == actor && x.TvShow == tvShow);

            if (assingmentExists)
            {
                response.AddError(Model.Actor, Error.actor_Assignment_Exists);
                return response;
            }

            var assingment = new ActorsAssignments();
            assingment.Actor = actor;
            assingment.TvShow = tvShow;

            var result = await _actorsAssignmentsRepository.AddAsync(assingment);
            if (!result)
            {
                response.AddError(Model.Assignment, Error.assignment_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> RemoveActorAssignment(int assignmentId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var assignmentExists = await _actorsAssignmentsRepository.ExistAsync(x => x.Id == assignmentId);
            if (!assignmentExists)
            {
                response.AddError(Model.Assignment, Error.assignment_Deleting);
                return response;
            }

            var assignment = await _actorsAssignmentsRepository.GetByAsync(x => x.Id == assignmentId);

            var result = await _actorsAssignmentsRepository.Remove(assignment);
            if (!result)
            {
                response.AddError(Model.Assignment, Error.assingment_Not_Exists);
                return response;
            }

            return response;
        }
    }
}
