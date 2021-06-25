namespace TelegramBot.Domain
{
    public class Song : IEntity
    {
        public int Id { get; }
        
        //TODO: Index to Name
        //TODO: Name + Author unique constraint
        public string Name { get; init; }
        public string Author { get; set; }
        public string Beat { get; init; }
        
        //TODO: IEnumerable<Chords>
        public string Chords { get; init; }
        public string Capo { get; init; }
        public string Text { get; init; }

        public override string ToString()
        {
            return $"Song {Name},\nCapo {Capo},\nBeat {Beat},\nChords {Chords},\n\nText: \n {Text}";
        }
    }
}