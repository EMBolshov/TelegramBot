namespace TelegramBot.Domain
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beat { get; set; }
        public string Chords { get; set; }
        public string Capo { get; set; }
        public string Text { get; set; }
    }
}