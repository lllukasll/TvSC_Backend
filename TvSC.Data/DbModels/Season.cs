using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Season : BaseModel
    {
        public virtual TvShow TvShow { get; set; }
        public int TvShowId { get; set; }
        public int SeasonNumber { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
