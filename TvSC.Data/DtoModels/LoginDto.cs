using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class LoginDto : BaseModelDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
