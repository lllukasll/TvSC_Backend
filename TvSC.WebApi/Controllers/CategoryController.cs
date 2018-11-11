using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvSC.Data.BindingModels.Category;
using TvSC.Services.Interfaces;
using TvSC.WebApi.Helpers;

namespace TvSC.WebApi.Controllers
{
    [Route("Category")]
    [CustomActionFilters]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategories();
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("categoryId")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var result = await _categoryService.GetCategory(categoryId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryBindingModel addCategoryBindingModel)
        {
            var result = await _categoryService.AddCategory(addCategoryBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
