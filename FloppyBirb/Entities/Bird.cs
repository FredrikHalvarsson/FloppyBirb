using System;
using FloppyBirb.Strategies;

namespace FloppyBirb.Entities
{
    // Strategy Pattern: Bird movement is implemented with the IMovementStrategy, with this we can easily implement sereval types of movement
    public class Bird
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        private float DY;
        private IMovementStrategy MovementStrategy;

        public Bird(float x, float y, IMovementStrategy movementStrategy)
        {
            X = x;
            Y = y;
            MovementStrategy = movementStrategy;
            DY = 0;
        }

        // Utilizations of the movement strategy
        public void Flap()
        {
            DY = MovementStrategy.Flap();
        }

        public void Update()
        {
            MovementStrategy.Update(ref DY);
            Y += DY;
        }

        public void Render(string BirdUp, string BirdDown)
        {
            if ((int)Y < Console.WindowHeight - 1 && (int)Y >= 0)
            {
                bool verticalVelocity = DY < 0;
                Console.SetCursorPosition((int)X - 3, (int)Y);
                Console.Write(verticalVelocity ? BirdUp : BirdDown);
            }
        }

        public void Erase()
        {
            Console.SetCursorPosition((int)(X) - 3, (int)Y);
            Console.Write("      ");
        }
    }
}