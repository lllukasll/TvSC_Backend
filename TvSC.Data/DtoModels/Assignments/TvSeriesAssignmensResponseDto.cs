using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Actor;

namespace TvSC.Data.DtoModels.Assignments
{
    public class TvSeriesAssignmensResponseDto : BaseModelDto
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public ActorDto ActorDto { get; set; }
    }
}
