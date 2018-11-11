using System;
using System.Collections.Generic;
using System.Text;
using TvSC.Data.DtoModels.Actor;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.DtoModels.Season;

namespace TvSC.Data.DtoModels.TvShow
{
    public class TvShowResponse : BaseModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
        public string PhotoName { get; set; }
        public ICollection<SeasonDto> Seasons { get; set; }
        public ICollection<ActorDto> Actors { get; set; }
        public ICollection<CategoryResponse> Categories { get; set; }
        public TvSeriesRatingsDto TvSeriesRatings { get; set; }
    }
}
