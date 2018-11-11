using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class TvShowCategoriesAssignmentsService : ITvShowCategoriesAssignmentsService
    {
        private readonly IRepository<TvShowCategoryAssignments> _tvShowCategoryAssignemtsRepository;
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public TvShowCategoriesAssignmentsService(IRepository<TvShowCategoryAssignments> tvShowCategoryAssignemtsRepository, IRepository<TvShow> tvShowRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _tvShowCategoryAssignemtsRepository = tvShowCategoryAssignemtsRepository;
            _tvShowRepository = tvShowRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<CategoryResponse>> GetTvShowsCategories(int tvShowId)
        {
            var response = new ResponsesDto<CategoryResponse>();

            var tvShowExists = await _tvShowRepository.ExistAsync(x => x.Id == tvShowId);
            if (!tvShowExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var assignments = _tvShowCategoryAssignemtsRepository.GetAllBy(x => x.TvShow.Id == tvShowId, x => x.Category);
            var assignmentsMapped = new List<CategoryResponse>();

            foreach (var assignment in assignments)
            {
                var category = await _categoryRepository.GetByAsync(x => x.Id == assignment.Category.Id);
                var categoryMapped = _mapper.Map<CategoryResponse>(category);
                assignmentsMapped.Add(categoryMapped);
            }

            response.DtoObject = assignmentsMapped;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> DeleteCategoryAssignment(int categoryAssignmentId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var categoryAssignmentExists =
                await _tvShowCategoryAssignemtsRepository.ExistAsync(x => x.Id == categoryAssignmentId);
            if (!categoryAssignmentExists)
            {
                response.AddError(Model.CategoryAssignment, Error.categoryAssignment_NotFound);
                return response;
            }

            var categoryAssignment = await _tvShowCategoryAssignemtsRepository.GetByAsync(x => x.Id == categoryAssignmentId);

            var result = await _tvShowCategoryAssignemtsRepository.Remove(categoryAssignment);
            if (!result)
            {
                response.AddError(Model.CategoryAssignment, Error.categoryAssignment_Deleting);
                return response;
            }

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddCategoryAssignment(int categoryId, int tvShowId)
        {
            var response = new ResponseDto<BaseModelDto>();

            var categoryExists = await _categoryRepository.ExistAsync(x => x.Id == categoryId);
            if (!categoryExists)
            {
                response.AddError(Model.Category, Error.category_NotFound);
                return response;
            }

            var tvShowExists = await _tvShowRepository.ExistAsync(x => x.Id == tvShowId);
            if (!tvShowExists)
            {
                response.AddError(Model.TvShow, Error.tvShow_NotFound);
                return response;
            }

            var tvShow = await _tvShowRepository.GetByAsync(x => x.Id == tvShowId);
            var category = await _categoryRepository.GetByAsync(x => x.Id == categoryId);

            var categoryAssignmentExists =
                await _tvShowCategoryAssignemtsRepository.ExistAsync(x => x.Category == category && x.TvShow == tvShow);

            if (categoryAssignmentExists)
            {
                response.AddError(Model.CategoryAssignment, Error.categoryAssignment_Already_Exists);
                return response;
            }

            var categoryAssignment = new TvShowCategoryAssignments();
            categoryAssignment.TvShow = tvShow;
            categoryAssignment.Category = category;

            var result = await _tvShowCategoryAssignemtsRepository.AddAsync(categoryAssignment);
            if (!result)
            {
                response.AddError(Model.CategoryAssignment, Error.categoryAssignment_Adding);
                return response;
            }

            return response;
        }
    }
}
