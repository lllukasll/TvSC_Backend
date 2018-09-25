using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TvSC.Data.DbModels;
using TvSC.Repo.Repositories;

namespace TvSC.Services.Services
{
    public class RatingService
    {
        private readonly Repository<TvSeriesRating> _tvSeriesRatingRepository;
        private readonly IMapper _mapper;

        public RatingService(Repository<TvSeriesRating> tvSeriesRatingRepository, IMapper mapper)
        {
            _tvSeriesRatingRepository = tvSeriesRatingRepository;
            _mapper = mapper;
        }


    }
}
