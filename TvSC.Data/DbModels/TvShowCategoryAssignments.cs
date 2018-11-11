using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class TvShowCategoryAssignments : BaseModel
    {
        public TvShow TvShow { get; set; }
        public Category Category { get; set; }
    }
}
