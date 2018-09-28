using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Data.DtoModels.WatchedEpisodes
{
    public class WatchedEpisodesResponseDto : BaseModelDto
    {
        public WatchedTvSeriesDto WatchedTvSeriesEpisodes { get; set; }
    }
}
