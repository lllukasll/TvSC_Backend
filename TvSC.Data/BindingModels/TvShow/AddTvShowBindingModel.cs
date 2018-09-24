using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TvSC.Data.Keys;

namespace TvSC.Data.BindingModels.TvShow
{
    public class AddTvShowBindingModel
    {
        [Required(ErrorMessage = Error.tvShow_Name_Required)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Network { get; set; }
        public int EpisodeLength { get; set; }
        public string EmissionHour { get; set; }
    }
}
