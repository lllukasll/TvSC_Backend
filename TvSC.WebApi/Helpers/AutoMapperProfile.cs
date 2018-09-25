using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using TvSC.Data.BindingModels;
using TvSC.Data.BindingModels.Episode;
using TvSC.Data.BindingModels.Season;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels.Episodes;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.DtoModels.TvShow;

namespace TvSC.WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMappings();
        }

        public void CreateMappings()
        {
            CreateMap<TvShow, AddTvShowBindingModel>().ReverseMap();
            CreateMap<TvShow, UpdateTvShowBindingModel>().ReverseMap();
            CreateMap<TvShow, TvShowResponse>().ReverseMap();
            CreateMap<TvShow, TvShowDto>().ReverseMap();

            CreateMap<Season, SeasonResponse>().ReverseMap();
            CreateMap<Season, SeasonResponseDto>().ReverseMap();
            CreateMap<Season, AddSeasonBindingModel>().ReverseMap();

            CreateMap<Episode, EpisodeDto>().ReverseMap();
            CreateMap<Episode, AddEpisodeBindingModel>().ReverseMap();
            CreateMap<Episode, UpdateEpisodeBindingModel>().ReverseMap();
            CreateMap<Episode, ReturnEpisodeDto>()
                .ForMember(dest => dest.SeasonNumber, opt => opt.MapFrom(x => x.SeasonNumber))
                .ForPath(dest => dest.TvShowName, opt => opt.MapFrom(x => x.Season.TvShow.Name));
        }
    }
}
