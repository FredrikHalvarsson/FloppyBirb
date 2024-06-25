namespace FloppyBirb.Game
{
    public class Program
    {
        // Design Patterns Used:

        //1. Singleton: Used for the `Game` class to ensure only one instance of the game is created.
        //2. Factory Method: Used for creating `Pipe` objects.
        //3. Strategy: Used for the bird's movement behavior.
        public static void Main(string[] args)
        {
            Game.Instance.Start();
        }
    }
}