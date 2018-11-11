using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Services.Interfaces
{
    public interface ITvShowCategoriesAssignmentsService
    {
        Task<ResponsesDto<CategoryResponse>> GetTvShowsCategories(int tvShowId);
        Task<ResponseDto<BaseModelDto>> DeleteCategoryAssignment(int categoryAssignmentId);
        Task<ResponseDto<BaseModelDto>> AddCategoryAssignment(int categoryId, int tvShowId);
    }
}