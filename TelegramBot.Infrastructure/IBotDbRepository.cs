using TelegramBot.Domain;

namespace TelegramBot.Infrastructure
{
    public interface IBotDbRepository
    {
        void AddChord(Chord chord);
        void AddSong(Song song);
    }
}