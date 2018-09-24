﻿using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Season;

namespace TvSC.Data.DtoModels.TvShow
{
    public class TvShowResponse : BaseModelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
        public ICollection<SeasonDto> Seasons { get; set; }
    }
}