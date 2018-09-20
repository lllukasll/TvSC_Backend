using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class TvShow : BaseModel
    {
        public string Hour { get; set; }
        public string Name { get; set; }
        public string Episode { get; set; }
        public DateTime Date { get; set; }

    }
}
