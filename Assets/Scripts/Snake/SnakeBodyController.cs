namespace Snake
{
    public class SnakeBodyController
    {
        public int Size { get; private set; } = 0;

        public void Eat()
        {
            Size += 1;
        }
    }
}