using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TvSC.Data.DbModels;

namespace TvSC.Repo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TvSeriesUserRating> TvSeriesUserRatings { get; set; }
        public DbSet<TvSeriesRatings> TvSeriesRatings { get; set; }
        public DbSet<UserFavouriteTvShows> UserFavouriteTvShows { get; set; }
        public DbSet<UserWatchedEpisode> UserWatchedTvSeries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(b => b.TvSeriesRatings)
                .WithOne(b => b.User);

            builder.Entity<User>()
                .HasMany(b => b.UserFavouriteTvShows)
                .WithOne(b => b.User);

            builder.Entity<User>()
                .HasMany(b => b.UserWatchedEpisodes)
                .WithOne(b => b.User);

            builder.Entity<User>()
                .Ignore(b => b.PhoneNumberConfirmed)
                .Ignore(b => b.PhoneNumber)
                .ToTable("Users");

            builder.Entity<TvShow>()
                .HasMany(x => x.Seasons);

            builder.Entity<TvShow>()
                .HasMany(b => b.UserFavouriteTvShows)
                .WithOne(b => b.TvShow);

            builder.Entity<TvShow>()
                .HasMany(b => b.TvSeriesUserRatings)
                .WithOne(b => b.TvShow);

            builder.Entity<TvShow>()
                .HasOne(b => b.TvSeriesRatings);

            builder.Entity<Season>()
                .HasMany(x => x.Episodes);

            builder.Entity<Episode>()
                .HasOne(b => b.Season);   
               

            base.OnModelCreating(builder);
        }

        
    }
}
