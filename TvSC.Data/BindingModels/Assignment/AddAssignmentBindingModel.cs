using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.BindingModels.Assignment
{
    public class AddAssignmentBindingModel
    {
        public int actorId { get; set; }
        public int tvShowId { get; set; }
        public string CharacterName { get; set; }
    }
}
