using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using TvSC.Data.BindingModels;
using TvSC.Data.BindingModels.Actor;
using TvSC.Data.BindingModels.Category;
using TvSC.Data.BindingModels.Episode;
using TvSC.Data.BindingModels.Rating;
using TvSC.Data.BindingModels.Season;
using TvSC.Data.BindingModels.TvShow;
using TvSC.Data.DbModels;
using TvSC.Data.DtoModels.Category;
using TvSC.Data.DtoModels.Episodes;
using TvSC.Data.DtoModels.FavouriteTvSeries;
using TvSC.Data.DtoModels.Rating;
using TvSC.Data.DtoModels.Season;
using TvSC.Data.DtoModels.TvShow;
using TvSC.Data.DtoModels.WatchedEpisodes;

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
            CreateMap<AddTvShowBindingModel, TvShow>()
                .ForMember(x => x.BackgroundPhotoName, opt => opt.Ignore())
                .ForMember(x => x.PhotoName, opt => opt.Ignore()); ;

            CreateMap<UpdateTvShowBindingModel, TvShow>()
                .ForMember(x => x.BackgroundPhotoName, opt => opt.Ignore())
                .ForMember(x => x.PhotoName, opt =>opt.Ignore());

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
                .ForPath(dest => dest.TvShowName, opt => opt.MapFrom(x => x.Season.TvShow.Name))
                .ForPath(dest => dest.BackgroundPhotoName, opt => opt.MapFrom(x => x.Season.TvShow.BackgroundPhotoName))
                .ForPath(dest => dest.PhotoName, opt => opt.MapFrom(x => x.Season.TvShow.PhotoName))
                .ForPath(dest => dest.Description, opt => opt.MapFrom(x => x.Season.TvShow.Description))
                .ForPath(dest => dest.TvShowId, opt => opt.MapFrom(x => x.Season.TvShow.Id))
                .ForPath(dest => dest.TvSeriesRatings, opt => opt.MapFrom(x => x.Season.TvShow.TvSeriesRatings));

            CreateMap<AddTvSeriesRatingBindingModel, TvSeriesUserRating>();
            CreateMap<UpdateTvSeriesRatingBindingModel, TvSeriesUserRating>();
            CreateMap<TvSeriesUserRating, GetTvSeriesRatingDto>();
            CreateMap<TvSeriesRatings, GetTvSeriesRatingDto>();

            CreateMap<UserFavouriteTvShows, FavouriteTvSeriesResponseDto>();

            CreateMap<UserWatchedEpisode, WatchedEpisodesResponseDto>()
                .ForPath(dest => dest.WatchedTvSeriesEpisodes, opt => opt.MapFrom(x => x.Episode.Season.TvShow));

            CreateMap<AddActorBindingModel, Actor>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<AddCategoryBindingModel, Category>();

            CreateMap<Category, CategoryResponse>();
            //CreateMap<UserWatchedEpisode, watched>();

        }
    }
}
