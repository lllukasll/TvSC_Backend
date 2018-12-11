using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Episodes;

namespace TvSC.Data.DtoModels.Season
{
    public class SeasonDto : BaseModelDto
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public ICollection<EpisodeDto> Episodes { get; set; }
        public bool Watched { get; set; }
    }
}
