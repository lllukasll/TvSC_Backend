using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Data.DtoModels.Episodes
{
    public class ReturnEpisodeDto : BaseModelDto
    {
        public string TvShowName { get; set; }
        public int SeasonNumber { get; set; }
        //public virtual SeasonForCalendarDto Season { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime AiringDate { get; set; }
        public TvSeriesRatingsDto TvSeriesRatings { get; set; }
        public string BackgroundPhotoName { get; set; }
        public string Description { get; set; }
    }
}
