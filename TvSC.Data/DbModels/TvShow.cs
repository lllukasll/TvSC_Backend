﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class TvShow : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}
