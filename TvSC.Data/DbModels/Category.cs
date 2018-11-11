using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DbModels
{
    public class Category : BaseModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
