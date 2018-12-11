using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Notification : BaseModel
    {
        public string Type { get; set; }
        public string FirstPart { get; set; }
        public string SecondPart { get; set; }
   
        public DateTime CreateDateTime { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }
    }
}
