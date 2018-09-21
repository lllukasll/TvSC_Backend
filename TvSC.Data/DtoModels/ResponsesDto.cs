using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class ResponsesDto<T> where T : BaseModelDto
    {
        public List<T> Object { get; set; }
        public bool ErrorOccurred => Errors.Any();
        public List<string> Errors { get; set; }

        public ResponsesDto()
        {
            Errors = new List<string>();
        }
    }
}
