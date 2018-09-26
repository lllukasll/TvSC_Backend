using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class UserFavouriteTvShows : BaseModel
    {
        public TvShow TvShow { get; set; }
        public int TvShowId { get; set; }
        public User User { get; set; }
        public string  UserId { get; set; }
    }
}
