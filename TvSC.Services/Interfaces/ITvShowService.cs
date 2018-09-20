using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvSC.Data.DbModels;

namespace TvSC.Services.Interfaces
{
    public interface ITvShowService
    {
        Task<List<TvShow>> GetAllTvShows();
    }
}
