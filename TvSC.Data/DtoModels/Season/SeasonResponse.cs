using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DbModels;

namespace TvSC.Data.DtoModels.Season
{
    public class SeasonResponse : BaseModelDto
    {
        public int SeasonNumber { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
