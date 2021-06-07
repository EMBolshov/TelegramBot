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

        public void AddChord(Chord chord)
        {
            AddEntity(chord);
        }
        
        public void AddSong(Song song)
        {
            AddEntity(song);
        }

        private static void AddEntity(IEntity entity)
        {
            using (var context = new BotDbContext(_connectionString))
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }
    }
}