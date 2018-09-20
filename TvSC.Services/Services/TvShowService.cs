using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvSC.Data.DbModels;
using TvSC.Repo.Interfaces;
using TvSC.Services.Interfaces;

namespace TvSC.Services.Services
{
    public class TvShowService : ITvShowService
    {
        private readonly IRepository<TvShow> _tvShowRepository;
        public TvShowService(IRepository<TvShow> tvShowRepository)
        {
            _tvShowRepository = tvShowRepository;
        }

        public async Task<List<TvShow>> GetAllTvShows()
        {
            var tvShows = await _tvShowRepository.GetAll().ToListAsync();
            return tvShows;
        }
        
    }
}
