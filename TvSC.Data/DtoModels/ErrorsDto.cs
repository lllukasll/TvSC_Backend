using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class ErrorsDto
    {
        public string Model;
        public IDictionary<string, string> Errors;
        public List<string> ErrorsTmp;
        public ErrorsDto()
        {
            Errors = new Dictionary<string, string>();
            ErrorsTmp = new List<string>();
        }
    }
}
