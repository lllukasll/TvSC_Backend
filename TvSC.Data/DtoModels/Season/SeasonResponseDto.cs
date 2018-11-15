using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels.Episodes;

namespace TvSC.Data.DtoModels.Season
{
    public class SeasonResponseDto : BaseModelDto
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public ICollection<EpisodeDto> Episodes { get; set; }
    }
}
