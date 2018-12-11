using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Episodes
{
    public class EpisodeDto : BaseModelDto
    {
        public int Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime AiringDate { get; set; }
        public bool Watched { get; set; }
    }
}
