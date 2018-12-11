using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class UserFavouriteCategoriesService : IUserFavouriteCategoriesService
    {
        private readonly IRepository<UserFavouriteCategories> _userFavouriteCategoriesRepository;
        private readonly IMapper _mapper;

        public UserFavouriteCategoriesService(IRepository<UserFavouriteCategories> userFavouriteCategoriesRepository, IMapper mapper)
        {
            _userFavouriteCategoriesRepository = userFavouriteCategoriesRepository;
            _mapper = mapper;
        }
    }
}
