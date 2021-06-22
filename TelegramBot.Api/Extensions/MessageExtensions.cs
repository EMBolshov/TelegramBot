using System;
using Telegram.Bot.Types;
using TelegramBot.Domain;

namespace TelegramBot.Api.Extensions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// /savechord Am e:-||-O-|---|---|&h:-||---|---|-O-|&g:-||---|---|-O-|&d:-||---|---|-O-|&A:-||-O-|---|---|&E:-||-O-|---|---|
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Chord ParseChord(this Message message)
        {
            //TODO: Do something with fingering format
            var parts = message.Text.Split(' ');
            
            if (parts.Length != 3)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            return new Chord
            {
                Name = parts[1],
                Fingering = parts[2]
            };
        }
        
        /// <summary>
        /// Format: /savesong|Name|Beat|Chords|Capo|Text
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Song ParseSong(this Message message)
        {
            //TODO: Format differs from other commands - split by | instead of whitespace 

            //Example: savesong|Дайте Танк (!) - Бардак|Восьмерка|Em, D#, C, Bm, F#, B, Am, G, D|0|Число фонарей умножая на два ...
            
            var parts = message.Text.Split('|');
            if (parts.Length != 6)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            return new Song
            {
                Name = parts[1],
                Beat = parts[2],
                Chords = parts[3],
                Capo = parts[4],
                Text = parts[5]
            };
        }

        /// <summary>
        /// Format: /getN name
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ParseName(this Message message)
        {
            var parts = message.Text.Split(' ');
            if (parts.Length != 2)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");
            
            return parts[1];
        }
    }
}