using System.Threading.Tasks;
using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public interface IBotDbRepository
    {
        Task AddChord(Chord chord);
        Task<Chord> GetChord(string name);
        Task AddSong(Song song);
    }
}