using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Actor
{
    public class ActorDto : BaseModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
    }
}
