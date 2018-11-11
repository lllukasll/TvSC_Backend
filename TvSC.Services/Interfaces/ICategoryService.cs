using System.Threading.Tasks;
using TvSC.Data.BindingModels.Category;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;

namespace TvSC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto<CategoryResponse>> GetCategory(int categoryId);
        Task<ResponsesDto<CategoryResponse>> GetCategories();
        Task<ResponseDto<BaseModelDto>> AddCategory(AddCategoryBindingModel addCategoryBindingModel);
    }
}