using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.Services.Interfaces
{
    public interface ITvShowService
    {
        Task<ResponseDto<SortedTvShowsResponse>> GetAllTvShows(SearchByParameters parameters);
        Task<ResponseDto<TvShowResponse>> GetTvSeries(int tvSeriesId);
        Task<ResponseDto<BaseModelDto>> AddTvShow(AddTvShowBindingModel tvShowBindingModel);
        Task<ResponseDto<BaseModelDto>> UpdateTvShow(int id, UpdateTvShowBindingModel tvShowBindingModel);
        Task<ResponseDto<BaseModelDto>> DeleteTvShow(int tvSeriesId);
    }
}
