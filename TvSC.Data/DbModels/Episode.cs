using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Episode : BaseModel
    {
        public virtual Season Season { get; set; }
        public int SeasonId { get; set; }
        public int SeasonNumber { get; set; }
        public int  EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime AiringDate { get; set; }
    }
}
