using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public interface IBotDbRepository
    {
        Task AddChord(Chord chord);
        Task<Chord> GetChord(string name);
        Task<IEnumerable<Chord>> GetChords(IEnumerable<string> names);
        Task AddSong(Song song);
        Task<Song> GetSong(string author, string name);
        Task<IEnumerable<Chord>> GetChordsForSong(string author, string name);
    }
}