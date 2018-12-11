using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class GetNotificationsDto: BaseModelDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string FirstPart { get; set; }
        public string SecondPart { get; set; }
        public string TvShowName { get; set; }
        public int TvShowId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
