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
    [Migration("20180925105113_AddedTvSeriesRatingModelV2")]
    partial class AddedTvSeriesRatingModelV2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Average");

                    b.Property<int>("Effects");

                    b.Property<int>("Music");

                    b.Property<int>("Story");

                    b.Property<int>("TvShowId");

                    b.Property<int>("UserId");

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("TvShowId");

                    b.HasIndex("UserId1");

                    b.ToTable("TvSeriesRatings");
                });

            modelBuilder.Entity("TvSC.Data.DbModels.TvShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("EmissionHour");

                    b.Property<int>("EpisodeLength");

                    b.Property<string>("Name");

                    b.Property<string>("Network");

                    b.HasKey("Id");

                    b.ToTable("TvShows");
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

            modelBuilder.Entity("TvSC.Data.DbModels.TvSeriesRating", b =>
                {
                    b.HasOne("TvSC.Data.DbModels.TvShow", "TvShow")
                        .WithMany("TvSeriesRatings")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TvSC.Data.DbModels.User", "User")
                        .WithMany("TvSeriesRatings")
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
