using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class UserWatchedEpisode : BaseModel
    {
        public Episode Episode { get; set; }
        public int EpisodeId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
