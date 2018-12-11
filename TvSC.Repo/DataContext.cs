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
        public DbSet<Actor> Actors { get; set; }
        public DbSet<TvShowCategoryAssignments> TvShowCategoryAssignmentsEntity { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TvSeriesUserRating> TvSeriesUserRatings { get; set; }
        public DbSet<TvSeriesRatings> TvSeriesRatings { get; set; }
        public DbSet<UserFavouriteTvShows> UserFavouriteTvShows { get; set; }
        public DbSet<UserWatchedEpisode> UserWatchedTvSeries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserFavouriteCategories> UserFavouriteCategories { get; set; }

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
                .HasMany(x => x.Categories);

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

            builder.Entity<ActorsAssignments>()
                .HasOne(x => x.TvShow);

            builder.Entity<ActorsAssignments>()
                .HasOne(x => x.Actor);

            builder.Entity<TvShowCategoryAssignments>()
                .HasOne(x => x.TvShow);

            builder.Entity<TvShowCategoryAssignments>()
                .HasOne(x => x.Category);

            builder.Entity<Comment>()
                .HasOne(x => x.TvShow);

            builder.Entity<Comment>()
                .HasOne(x => x.User);

            builder.Entity<Notification>()
                .HasOne(x => x.User);

            builder.Entity<Notification>()
                .HasOne(x => x.TvShow);

            builder.Entity<UserFavouriteTvShows>()
                .HasOne(x => x.TvShow);

            builder.Entity<UserFavouriteCategories>()
                .HasOne(x => x.Category);

            builder.Entity<UserFavouriteCategories>()
                .HasOne(x => x.User);

            base.OnModelCreating(builder);
        }

        
    }
}
