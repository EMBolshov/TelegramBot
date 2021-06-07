using Telegram.Bot.Types;
using TelegramBot.Domain;

namespace TelegramBot.Api.Extensions
{
    public static class MessageExtensions
    {
        public static Chord ParseChord(this Message message)
        {
            //TODO: Do something with fingering format

            //Format:
            /*savechord Am e:-||-O-|---|---| (but fingering is in one line)
                           h:-||---|---|-O-|
                           g:-||---|---|-O-|
                           d:-||---|---|-O-|
                           A:-||-O-|---|---|
                           E:-||-O-|---|---|
             */
            
            var parts = message.Text.Split(' ');

            return new Chord
            {
                Name = parts[1],
                Fingering = parts[2]
            };
        }

        public static Song ParseSong(this Message message)
        {
            //Format: savesong Name Beat Chords Capo Text
            //Example: savesong|Дайте Танк (!) - Бардак|Восьмерка|Em, D#, C, Bm, F#, B, Am, G, D|0|Число фонарей умножая на два ...
            
            var parts = message.Text.Split('|');
            
            return new Song
            {
                Name = parts[1],
                Beat = parts[2],
                Chords = parts[3],
                Capo = parts[4],
                Text = parts[5]
            };
        }
    }
}