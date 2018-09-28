using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Episodes
{
    public class EpisodeWatchedDto
    {
        public int EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime AiringDate { get; set; }
        public bool Watched { get; set; }
    }
}
