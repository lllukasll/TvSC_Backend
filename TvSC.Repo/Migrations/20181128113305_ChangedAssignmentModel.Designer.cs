﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvSC.Repo;

namespace TvSC.Repo.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181128113305_ChangedAssignmentModel")]
    partial class ChangedAssignmentModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TvSC.Data.DbModels.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Photo");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.ActorsAssignments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActorId");

                    b.Property<string>("CharacterName");

                    b.Property<int?>("TvShowId");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("TvShowId");

                    b.ToTable("ActorsAssignments");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TvShowId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AiringDate");

                    b.Property<string>("EpisodeName");

                    b.Property<int>("EpisodeNumber");

                    b.Property<int>("SeasonId");

                    b.Property<int>("SeasonNumber");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("TvShowId");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesRatings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Average");

                    b.Property<int>("Effects");

                    b.Property<int>("Music");

                    b.Property<int>("Story");

                    b.Property<int>("TvShowId");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId")
                        .IsUnique();

                    b.ToTable("TvSeriesRatings");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesUserRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Average");

                    b.Property<int>("Effects");

                    b.Property<int>("Music");

                    b.Property<int>("Story");

                    b.Property<int>("TvShowId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.HasIndex("UserId");

                    b.ToTable("TvSeriesUserRatings");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackgroundPhotoName");

                    b.Property<string>("Description");

                    b.Property<string>("EmissionHour");

                    b.Property<int>("EpisodeLength");

                    b.Property<string>("LongDescription");

                    b.Property<string>("Name");

                    b.Property<string>("Network");

                    b.Property<string>("PhotoName");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("TvShows");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvShowCategoryAssignments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("TvShowId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TvShowId");

                    b.ToTable("TvShowCategoryAssignmentsEntity");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.UserFavouriteTvShows", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TvShowId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFavouriteTvShows");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.UserWatchedEpisode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EpisodeId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserWatchedTvSeries");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.ActorsAssignments", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");

                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany()
                        .HasForeignKey("TvShowId");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Category", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow")
                        .WithMany("Categories")
                        .HasForeignKey("TvShowId");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Episode", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TvSC.Data.DbModels.Season", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany("Seasons")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesRatings", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithOne("TvSeriesRatings")
                        .HasForeignKey("TvSC.Data.DbModels.TvSeriesRatings", "TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesUserRating", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany("TvSeriesUserRatings")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TvSC.Data.DbModels.User", "User")
                        .WithMany("TvSeriesRatings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvShowCategoryAssignments", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany()
                        .HasForeignKey("TvShowId");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.UserFavouriteTvShows", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany("UserFavouriteTvShows")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TvSC.Data.DbModels.User", "User")
                        .WithMany("UserFavouriteTvShows")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.UserWatchedEpisode", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.Episode", "Episode")
                        .WithMany()
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TvSC.Data.DbModels.User", "User")
                        .WithMany("UserWatchedEpisodes")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
