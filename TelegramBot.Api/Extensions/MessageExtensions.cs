using System;
using System.Linq;
using Telegram.Bot.Types;
using TelegramBot.Domain;

namespace TelegramBot.Api.Extensions
{
    public static class MessageExtensions
    {
        public static string ParseCommand(this Message message)
        {
            var parts = message.Text.Split(new [] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries);
            return parts[0];
        }
        
        /// <summary>
        /// /savechord Am 0 e:O||---|---|---|---|&h:-||-O-|---|---|---|&g:-||---|-O-|---|---|&d:-||---|-O-|---|---|&A:O||---|---|---|---|&E:O||---|---|---|---|
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Chord ParseChord(this Message message)
        {
            var parts = message.Text.Split(' ');
            
            if (parts.Length != 4)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            var fingering = parts[3];

            if (!ValidateFingering(fingering))
                throw new ArgumentException($"Can not save chord {parts[1]} - fingering is in wrong format");
            
            return new Chord
            {
                Name = parts[1],
                StartFret = int.Parse(parts[2]),
                Fingering = fingering
            };

            bool ValidateFingering(string fingering)
            {
                var parts = fingering.Split('&', StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length != 6) return false;
                if (parts.Any(p => p.Length != 21)) return false;
                
                return true;
            }
        }
        
        /// <summary>
        /// Format: /savesong|Author|Name|Beat|Chords|Capo|Text
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Song ParseSong(this Message message)
        {
            //Example: savesong|Дайте Танк (!)|Бардак|Восьмерка|Em, D#, C, Bm, F#, B, Am, G, D|0|Число фонарей умножая на два ...
            
            var parts = message.Text.Split('|');
            if (parts.Length != 7)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");

            return new Song
            {
                Author = parts[1],
                Name = parts[2],
                Beat = parts[3],
                Chords = parts[4].Split(", ", StringSplitOptions.RemoveEmptyEntries),
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
        /// Format: /getsongN|author|name
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static (string name, string author) ParseSongAuthorAndName(this Message message)
        {
            var parts = message.Text.Split('|');
            if (parts.Length != 3)
                throw new ArgumentException($"Command {parts[0]} contain wrong number of arguments");
            
            return (parts[1], parts[2]);
        }
    }
}