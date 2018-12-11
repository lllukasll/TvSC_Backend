using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("UserFavouriteCategories")]
    [CustomActionFilters]
    public class UserFavouriteCategoriesController : Controller
    {
        private readonly IUserFavouriteCategoriesService _userFavouriteCategoriesService;

        public UserFavouriteCategoriesController(IUserFavouriteCategoriesService userFavouriteCategoriesService)
        {
            _userFavouriteCategoriesService = userFavouriteCategoriesService;
        }


    }
}
