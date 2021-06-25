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
        
        //TODO: Song.Author + Song.Name -> Unique
        //TODO: Chord.Name -> Unique
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chord>()
                .HasKey(c => c.Id)
                .HasName("Id");

            modelBuilder.Entity<Song>()
                .HasKey(s => s.Id)
                .HasName("Id");
        }
    }
}