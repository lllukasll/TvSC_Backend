using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using TvSC.Data.DtoModels;
using TvSC.Data.Keys;

namespace TvSC.WebApi.Helpers
{
    public class CustomActionFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = ModelStateErrors(filterContext);
            if (response.ErrorOccurred)
            {
                filterContext.Result = new BadRequestObjectResult(response);
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            PropertyInfo p = filterContext.Result.GetType().GetProperty("Value");

            if (p != null)
            {
                var rawResult = filterContext.Result.GetType().GetProperty("Value").GetValue(filterContext.Result);
                if (rawResult.GetType() != typeof(string))
                {

                    var errorObjects = rawResult.GetType().GetProperty("ErrorObjects").GetValue(rawResult);
                    ResponseDto<BaseModelDto> response = new ResponseDto<BaseModelDto>();
                    response.ErrorObjects = (List<ErrorsDto>)errorObjects;

                    if (response.ErrorOccurred)
                    {
                        filterContext.Result = new BadRequestObjectResult(response);
                    }
                }
            }
            base.OnResultExecuting(filterContext);
        }
        protected ResponseDto<BaseModelDto> ModelStateErrors(ActionExecutingContext filterContext)
        {
            Type errors = typeof(Error);
            var response = new ResponseDto<BaseModelDto>();
            var fieldsInErrors = typeof(Error).GetFields();
            List<string> fieldNamesInErrors = new List<string>();
            foreach (var field in fieldsInErrors)
            {
                fieldNamesInErrors.Add(field.GetValue(null).ToString());
            }
            foreach (var key in filterContext.ModelState.Keys)
            {
                var value = filterContext.ModelState[key];
                foreach (var error in value.Errors)
                {
                    if (fieldNamesInErrors.Contains(error.ErrorMessage))
                    {
                        response.AddError(key, error.ErrorMessage);
                    }
                    else
                    {
                        response.AddError(key, Error.data_Invalid);
                    }
                }
            }
            return response;
        }
    }
}
