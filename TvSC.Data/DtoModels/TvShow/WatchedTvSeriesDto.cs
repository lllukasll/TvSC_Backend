using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.DtoModels.Season;

namespace TvSC.Data.DtoModels.TvShow
{
    public class WatchedTvSeriesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
        public ICollection<SeasonWatchedDto> Seasons { get; set; }
    }
}
