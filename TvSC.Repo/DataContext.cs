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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(b => b.Id);

            builder.Entity<User>()
                .Ignore(b => b.PhoneNumberConfirmed)
                .Ignore(b => b.PhoneNumber)
                .ToTable("Users");

            base.OnModelCreating(builder);
        }

        
    }
}
