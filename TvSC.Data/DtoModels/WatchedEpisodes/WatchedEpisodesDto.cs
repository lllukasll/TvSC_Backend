using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.WatchedEpisodes
{
    public class WatchedEpisodesDto : BaseModelDto
    {
        public int Id { get; set; }
        public string TvShowName { get; set; }
        public string PhotoName { get; set; }
        public string Network { get; set; }
        public string EpisodeName { get; set; }
    }
}
