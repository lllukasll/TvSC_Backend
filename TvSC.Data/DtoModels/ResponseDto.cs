using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class ResponseDto<T> where T : BaseModelDto
    {
        public T Object { get; set; }
        public bool ErrorOccurred => Errors.Any();
        public List<string> Errors { get; set; }

        public ResponseDto()
        {
            Errors = new List<string>();
        }
    }
}
