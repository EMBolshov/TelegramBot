using System;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public class BotDbContext : DbContext
    {
        private readonly string _connectionString;
        
        public DbSet<Chord> Chords { get; set; }
        public DbSet<Song> Songs { get; set; }

        public BotDbContext()
        {
            
        }
        
        public BotDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupChords();

            SetupSong();

            void SetupChords()
            {
                modelBuilder.Entity<Chord>()
                    .HasKey(c => c.Id)
                    .HasName("Id");

                modelBuilder.Entity<Chord>().HasIndex(c => c.Name).IsUnique();
            }

            void SetupSong()
            {
                modelBuilder.Entity<Song>()
                    .HasKey(s => s.Id)
                    .HasName("Id");

                modelBuilder.Entity<Song>()
                    .Property(e => e.Chords)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

                modelBuilder.Entity<Song>()
                    .HasIndex(s => new {s.Author, s.Name})
                    .IsUnique();
            }
        }
    }
}