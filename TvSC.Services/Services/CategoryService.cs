using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.BindingModels.Category;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.Keys;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<TvShow> _tvShowRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<TvShow> tvShowRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _tvShowRepository = tvShowRepository;
            _mapper = mapper;
        }

        public async Task<ResponsesDto<CategoryResponse>> GetCategories()
        {
            var response = new ResponsesDto<CategoryResponse>();

            var categories = _categoryRepository.GetAll();
            var categoriesMapped = new List<CategoryResponse>();

            foreach (var category in categories)
            {
                categoriesMapped.Add(_mapper.Map<CategoryResponse>(category));
            }

            response.DtoObject = categoriesMapped;

            return response;
        }

        public async Task<ResponseDto<CategoryResponse>> GetCategory(int categoryId)
        {
            var response = new ResponseDto<CategoryResponse>();

            var categoryExists = await _categoryRepository.ExistAsync(x => x.Id == categoryId);
            if (!categoryExists)
            {
                response.AddError(Model.Category, Error.category_NotFound);
                return response;
            }

            var category = await _categoryRepository.GetByAsync(x => x.Id == categoryId);
            var categoryMapped = _mapper.Map<CategoryResponse>(category);

            response.DtoObject = categoryMapped;

            return response;
        }

        public async Task<ResponseDto<BaseModelDto>> AddCategory(AddCategoryBindingModel addCategoryBindingModel)
        {
            var response = new ResponseDto<BaseModelDto>();

            var categoryExists = await _categoryRepository.ExistAsync(x => x.Name == addCategoryBindingModel.Name);
            if (categoryExists)
            {
                response.AddError(Model.Category, Error.category_Already_Exists);
                return response;
            }

            var category = _mapper.Map<Category>(addCategoryBindingModel);

            var result = await _categoryRepository.AddAsync(category);
            if (!result)
            {
                response.AddError(Model.Category, Error.category_Adding);
                return response;
            }

            return response;
        }
    }
}
