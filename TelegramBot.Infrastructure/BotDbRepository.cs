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

        public async Task AddSong(Song song)
        {
            await AddEntity(song);
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