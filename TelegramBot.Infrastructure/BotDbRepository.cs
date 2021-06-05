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
            using (var context = new BotDbContext(_connectionString))
            {
                context.Add(chord);
                context.SaveChanges();
            }
        }
    }
}