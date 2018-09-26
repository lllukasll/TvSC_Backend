using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Data.DtoModels.FavouriteTvSeries
{
    public class FavouriteTvSeriesResponseDto : BaseModelDto
    {
        public TvShowResponse FavouriteTvShow { get; set; }
    }
}
