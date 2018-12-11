using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class UserStatsResponseDto : BaseModelDto
    {
        public int HoursWatched { get; set; }
        public int EpisodesWatched { get; set; }
        public int RatedCount { get; set; }
        public int LikedTvSeries { get; set; }
        public int CommentsCount { get; set; }
        public int AverageRating { get; set; }
    }
}
