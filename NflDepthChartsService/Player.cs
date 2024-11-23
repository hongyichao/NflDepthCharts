
namespace NflDepthChartsService
{
    public class Player
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public Player(int number, string name, string position)
        {
            Number = number;
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return $"#{Number} - {Name}";
        }


    }
}
