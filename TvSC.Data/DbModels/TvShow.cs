using System;
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
        public string PhotoName { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public virtual ICollection<TvSeriesUserRating> TvSeriesUserRatings { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual TvSeriesRatings TvSeriesRatings { get; set; }
        public virtual ICollection<UserFavouriteTvShows> UserFavouriteTvShows { get; set; }
    }
}
