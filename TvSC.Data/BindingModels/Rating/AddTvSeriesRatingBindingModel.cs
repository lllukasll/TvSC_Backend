using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.BindingModels.Rating
{
    public class AddTvSeriesRatingBindingModel
    {
        public int Story { get; set; }
        public int Music { get; set; }
        public int Effects { get; set; }
        public int Average { get; set; }
    }
}
