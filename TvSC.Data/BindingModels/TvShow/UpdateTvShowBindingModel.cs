using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TvSC.Data.BindingModels.TvShow
{
    public class UpdateTvShowBindingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile BackgroundPhoto { get; set; }
    }
}
