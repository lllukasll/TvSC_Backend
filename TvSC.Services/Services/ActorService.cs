using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Actor;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Actor;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IMapper _mapper;

        public ActorService(IRepository<Actor> actorRepository, IRepository<TvShow> tvShowRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _tvShowRepository = tvShowRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<ActorDto>> GetActors()
        {
            var response = new ResponsesDto<ActorDto>();

            var actors = _actorRepository.GetAll();

            var mappedActors = new List<ActorDto>();
            foreach (var actor in actors)
            {
                mappedActors.Add(_mapper.Map<ActorDto>(actor));
            }

            response.DtoObject = mappedActors;
            return response;
        }

        public async Task<ResponseDto<ActorDto>> GetActor(int actorId)
        {
            var response = new ResponseDto<ActorDto>();
            var actorExists = await _actorRepository.ExistAsync(x => x.Id == actorId);
            if (!actorExists)
            {
                response.AddError(Model.Actor, Error.actor_NotExists);
                return response;
            }

            var actor = await _actorRepository.GetByAsync(x => x.Id == actorId);
            var mappedActor = _mapper.Map<ActorDto>(actor);

            response.DtoObject = mappedActor;
            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddActor(AddActorBindingModel addActorBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();
            if (!Directory.Exists("wwwroot\\ActorsPictures"))
                Directory.CreateDirectory("wwwroot\\ActorsPictures");

            var fileName = Guid.NewGuid() + Path.GetExtension(addActorBindingModel.Photo.FileName);
            var filePath = Path.Combine("wwwroot\\ActorsPictures", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await addActorBindingModel.Photo.CopyToAsync(stream);
            }

            var actor = _mapper.Map<Actor>(addActorBindingModel);
            actor.Photo = fileName;

            var result = await _actorRepository.AddAsync(actor);
            if (!result)
            {
                File.Delete(filePath);
                response.AddError(Model.Actor, Error.actor_Adding);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteActor(int actorId)
        {
            var response = new ResponseDto<BaseModelDto>();
            var actorExists = await _actorRepository.ExistAsync(x => x.Id == actorId);
            if (!actorExists)
            {
                response.AddError(Model.Actor, Error.actor_NotExists);
                return response;
            }

            var actor = await _actorRepository.GetByAsync(x => x.Id == actorId);

            var result = await _actorRepository.Remove(actor);
            if (!result)
            {
                response.AddError(Model.Actor, Error.actor_Deleting);
                return response;
            }

            if (File.Exists("wwwroot\\ActorsPictures\\" + actor.Photo))
            {
                File.Delete("wwwroot\\ActorsPictures\\" + actor.Photo);
            }

            return response;

        }
    }
}
