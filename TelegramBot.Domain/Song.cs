namespace TelegramBot.Domain
{
    public class Song : IEntity
    {
        public int Id { get; }
        public string Name { get; init; }
        public string Beat { get; init; }
        public string Chords { get; init; }
        public string Capo { get; init; }
        public string Text { get; init; }
    }
}