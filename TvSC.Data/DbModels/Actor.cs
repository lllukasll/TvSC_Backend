using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Actor : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Photo { get; set; }
    }
}
