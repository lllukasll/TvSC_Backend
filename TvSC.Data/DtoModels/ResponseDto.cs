using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvSC.Data.DtoModels
{
    public class ResponseDto<T> where T : BaseModelDto
    {
        public T DtoObject { get; set; }

        public bool ErrorOccurred => ErrorObjects.Count > 0;

        public List<ErrorsDto> ErrorObjects { get; set; }

        public ResponseDto()
        {
            ErrorObjects = new List<ErrorsDto>();
        }

        public void Merge(ResponseDto<T> otherResponse)
        {
            foreach (var newError in otherResponse.ErrorObjects)
            {
                foreach (var existingError in ErrorObjects)
                    if (existingError.Model != newError.Model)
                    {
                        this.ErrorObjects.Add(newError);
                    }
                    else
                    {
                        foreach (var Error in newError.Errors)
                        {
                            if (!existingError.Errors.ContainsKey(Error.Key))
                                existingError.Errors.Add(Error);
                        }
                    }
            }
        }

        public void AddError(string Object, string errorKey)
        {
            bool objectExists = false;
            foreach (var ErrorObject in ErrorObjects)
            {
                if (ErrorObject.Model == Object)
                {
                    objectExists = true;
                    if (!ErrorObject.Errors.ContainsKey(errorKey))
                    {
                        ErrorObject.Errors.Add(errorKey, "");
                    }
                }
            }
            if (objectExists)
            {
                return;
            }
            IDictionary<string, string> ErrorForObjectToAdd = new Dictionary<string, string>();
            ErrorForObjectToAdd.Add(errorKey, "");
            ErrorObjects.Add(new ErrorsDto { Model = Object, Errors = ErrorForObjectToAdd });
        }
    }
}
