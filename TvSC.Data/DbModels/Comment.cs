using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Comment : BaseModel
    {
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TvSeriesId { get; set; }
        public TvShow TvShow { get; set; }
    }
}
