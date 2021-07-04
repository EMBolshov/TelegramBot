namespace TelegramBot.Domain
{
    public class Song : IEntity
    {
        public int Id { get; }
        public string Name { get; init; }
        public string Author { get; init; }
        public string Beat { get; init; }
        public string[] Chords { get; init; }
        public string Capo { get; init; }
        public string Text { get; init; }

        public override string ToString()
        {
            return $"Song {Name} by {Author},\nCapo {Capo},\nBeat {Beat},\nChords {string.Join(", ", Chords)},\n\nText: \n {Text}";
        }
    }
}