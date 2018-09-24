using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.BindingModels.Episode
{
    public class AddEpisodeBindingModel
    {
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime AiringDate { get; set; }
    }
}
