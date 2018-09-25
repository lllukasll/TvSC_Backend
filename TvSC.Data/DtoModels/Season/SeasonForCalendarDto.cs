using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Data.DtoModels.Season
{
    public class SeasonForCalendarDto
    {
        public int SeasonNumber { get; set; }
        public virtual TvShowDto TvShow { get; set; }
    }
}
