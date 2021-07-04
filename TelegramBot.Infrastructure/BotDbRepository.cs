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

        public async Task AddChord(Chord chord)
        {
            await AddEntity(chord);
        }

        public async Task<Chord> GetChord(string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Chords.SingleOrDefaultAsync(c => c.Name == name);
            }
        }

        public async Task<IEnumerable<Chord>> GetChords(IEnumerable<string> names)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return context.Chords.Where(c => names.Contains(c.Name)).ToList();
            }
        }

        public async Task AddSong(Song song)
        {
            await AddEntity(song);
        }

        public async Task<Song> GetSong(string author, string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                return await context.Songs.SingleOrDefaultAsync(s => s.Author == author && s.Name == name);
            }
        }

        public async Task<IEnumerable<Chord>> GetChordsForSong(string author, string name)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                var song = await context.Songs.SingleAsync(s => s.Author == author && s.Name == name);
                var chords = song.Chords;
                return context.Chords.Where(c => chords.Contains(c.Name));
            }
        }

        private static async Task AddEntity(IEntity entity)
        {
            await using (var context = new BotDbContext(_connectionString))
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}