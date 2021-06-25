using System;
using Telegram.Bot.Types;
using TelegramBot.Domain;

namespace TelegramBot.Api.Extensions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// /savechord Am 0 e:-||-O-|---|---|&h:-||---|---|-O-|&g:-||---|---|-O-|&d:-||---|---|-O-|&A:-||-O-|---|---|&E:-||-O-|---|---|
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Chord ParseChord(this Message message)
        {
            //TODO: Do something with fingering format
            var parts = message.Text.Split(' ');
            
            if (parts.Length != 4)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            return new Chord
            {
                Name = parts[1],
                StartFret = parts[2],
                Fingering = parts[3]
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

            //Example: savesong|Дайте Танк (!)|Бардак|Восьмерка|Em, D#, C, Bm, F#, B, Am, G, D|0|Число фонарей умножая на два ...
            
            var parts = message.Text.Split('|');
            if (parts.Length != 7)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            return new Song
            {
                Name = parts[1],
                Author = parts[2],
                Beat = parts[3],
                Chords = parts[4],
                Capo = parts[5],
                Text = parts[6]
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
        
        /// <summary>
        /// Format: /getsongN author name
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static (string name, string author) ParseSongAuthorAndName(this Message message)
        {
            var parts = message.Text.Split(' ');
            if (parts.Length != 3)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");
            
            return (parts[1], parts[2]);
        }
    }
}