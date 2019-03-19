using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Tweetify.Models;

namespace Tweetify.DAL
{
    public class TweetifyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL("server=localhost;database=tweetify2;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.HasMany(e => e.Tweets).WithOne(t => t.Author);
                entity.HasMany(e => e.Hightlights);
                entity.HasMany(e => e.Followers).WithOne(f => f.Following);
                entity.HasMany(e => e.Followings).WithOne(f => f.Follower);
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(e => e.Author).WithMany(u => u.Tweets).IsRequired();
                entity.HasOne(e => e.OriginalTweet).WithMany(t => t.Answers);
                entity.HasMany(e => e.Likes).WithOne(l => l.Tweet);
                entity.HasMany(e => e.Answers).WithOne(t => t.OriginalTweet);
                entity.Property(e => e.IsRetweet).HasConversion<int>();

            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User);
                entity.HasOne(e => e.Tweet).WithMany(t => t.Likes);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Follower).WithMany(u => u.Followings);
                entity.HasOne(e => e.Following).WithMany(u => u.Followers);
            });
        }
    }
}


