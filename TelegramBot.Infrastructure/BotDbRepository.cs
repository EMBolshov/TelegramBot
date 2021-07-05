using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public class BotDbRepository : IBotDbRepository
    {
        private static string _connectionString;

        public BotDbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddChordAsync(Chord chord)
        {
            await AddEntityAsync(chord);
        }

        public async Task<Chord> GetChordAsync(string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Chords.SingleOrDefaultAsync(c => c.Name == name);
            }
        }

        public async Task<IEnumerable<Chord>> GetChordsAsync(IEnumerable<string> names)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Chords.Where(c => names.Contains(c.Name)).ToListAsync();
            }
        }

        public async Task AddSongAsync(Song song)
        {
            await AddEntityAsync(song);
        }

        public async Task<Song> GetSongAsync(string author, string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Songs.SingleOrDefaultAsync(s => s.Author == author && s.Name == name);
            }
        }
        
        public async Task<IEnumerable<Song>> GetSongListAsync()
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Songs.ToListAsync();
            }
        }

        public async Task<IEnumerable<Chord>> GetChordsForSongAsync(string author, string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                var song = await context.Songs.SingleAsync(s => s.Author == author && s.Name == name);
                var chords = song.Chords;
                
                return await context.Chords.Where(c => chords.Contains(c.Name)).ToListAsync();
            }
        }

        private static async Task AddEntityAsync(IEntity entity)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}