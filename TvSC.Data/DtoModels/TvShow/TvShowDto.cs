﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.TvShow
{
    public class TvShowDto : BaseModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public string PhotoName { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
    }
}
