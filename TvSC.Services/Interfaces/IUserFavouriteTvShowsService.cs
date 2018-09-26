using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.FavouriteTvSeries;

namespace TvSC.Services.Interfaces
{
    public interface IUserFavouriteTvShowsService
    {
        Task<ResponseDto<BaseModelDto>> AddUserFavouriteTvSeries(int tvSeriesId, string userId);
        Task<ResponsesDto<FavouriteTvSeriesResponseDto>> GetUserFavouriteTvSeries(string userId);
        Task<ResponseDto<BaseModelDto>> DeleteUserFavouriteTvSeries(int favouriteTvSeriesId, string userId);
    }
}
