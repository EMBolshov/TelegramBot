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
            /*saveChord Am e:-||-O-|---|---| (but fingering is in one line)
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
    }
}