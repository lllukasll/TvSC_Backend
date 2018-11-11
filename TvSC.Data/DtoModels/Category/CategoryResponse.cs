using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels.Category
{
    public class CategoryResponse : BaseModelDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
