using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TvSC.Data.BindingModels.TvShow
{
    public class SearchByParameters 
    {
        public string[] Categories { get; set; }
        public string[] Networks { get; set; }
        public int Status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public SearchByParameters()
        {
            Status = 0;
            PageNumber = 1;
            PageSize = 20;
        }
    }
}
