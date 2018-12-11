using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Comment
{
    public class GetTvSeriesCommentsDto : BaseModelDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
