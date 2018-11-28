using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class ActorsAssignments : BaseModel
    {
        public Actor Actor { get; set; }
        public TvShow TvShow { get; set; }
        public string CharacterName { get; set; }
    }
}
