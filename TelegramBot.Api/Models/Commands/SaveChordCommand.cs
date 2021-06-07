﻿using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class SaveChordCommand : ICommand
    {
        private readonly IBotDbRepository _repository;
        
        public string Name => "saveChord";

        public SaveChordCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Message message, TelegramBotClient client)
        {
            _repository.AddChord(message.ParseChord());
        }
    }
}