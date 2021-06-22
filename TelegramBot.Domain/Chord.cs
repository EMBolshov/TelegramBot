namespace TelegramBot.Domain
{
    public class Chord : IEntity
    {
        public int Id { get; }
        public string Name { get; init; }
        public string Fingering { get; init; }

        public override string ToString()
        {
            return Fingering.Replace('&', '\n');
        }
    }
}