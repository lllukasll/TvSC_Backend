using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.DtoModels;
using TvSC.Data.Keys;

namespace TvSC.WebApi.Controllers
{
    public class BaseResponseController : Controller
    {
        protected ResponseDto<BaseModelDto> ModelStateErrors()
        {
            var response = new ResponseDto<BaseModelDto>();

            foreach (var key in ModelState.Keys)
            {
                var value = ViewData.ModelState[key];

                foreach (var error in value.Errors)
                {
                    if (error.Exception != null)
                    {
                        response.AddError(Model.DataFormat, "Nieprawidłowy format danych");
                    }
                    else
                    {
                        response.AddError(Model.DataFormat, error.ErrorMessage);
                    }
                }
            }
            return response;
        }
    }
}
