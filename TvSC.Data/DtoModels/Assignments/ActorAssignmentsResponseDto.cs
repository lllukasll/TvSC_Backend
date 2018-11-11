using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Data.DtoModels.Assignments
{
    public class ActorAssignmentsResponseDto : BaseModelDto
    {
        public TvShowResponse TvShow { get; set; }
    }
}
