using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Episodes;

namespace TvSC.Data.DtoModels.Season
{
    public class SeasonWatchedDto
    {
        public int SeasonNumber { get; set; }
        public ICollection<EpisodeWatchedDto> Episodes { get; set; }
    }
}
