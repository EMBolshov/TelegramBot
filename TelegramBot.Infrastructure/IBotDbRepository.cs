using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public interface IBotDbRepository
    {
        Task AddChordAsync(Chord chord);
        Task<Chord> GetChordAsync(string name);
        Task<IEnumerable<Chord>> GetChordsAsync(IEnumerable<string> names);
        Task AddSongAsync(Song song);
        Task<Song> GetSongAsync(string author, string name);
        Task<IEnumerable<Chord>> GetChordsForSongAsync(string author, string name);
    }
}