using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class TvSeriesRating : BaseModel
    {
        public int Story { get; set; }
        public int Music { get; set; }
        public int Effects { get; set; }
        public int Average { get; set; }

        public int TvShowId { get; set; }
        public virtual TvShow TvShow { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
