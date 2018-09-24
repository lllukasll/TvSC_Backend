using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class ErrorsDto
    {
        public string Model;
        public IDictionary<string, string> Errors;
        public ErrorsDto()
        {
            Errors = new Dictionary<string, string>();
        }
    }
}
