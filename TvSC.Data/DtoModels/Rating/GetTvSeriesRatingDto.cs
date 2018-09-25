using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Rating
{
    public class GetTvSeriesRatingDto : BaseModelDto
    {
        public int Story { get; set; }
        public int Music { get; set; }
        public int Effects { get; set; }
        public int Average { get; set; }
    }
}
