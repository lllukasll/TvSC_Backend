using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.TvShow
{
    public class SortedTvShowsResponse : BaseModelDto
    {
        public List<TvShowResponse> TvShowsList { get; set; }
        public int ActivePageNumber { get; set; }
        public int PageSize { get; set; }
        public double TotalPageNumber { get; set; }
    }
}
