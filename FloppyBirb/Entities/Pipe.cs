using System;
using FloppyBirb.Helpers;

namespace FloppyBirb.Entities
{
    public class Pipe
    {
        public int X { get; private set; }
        public int GapY { get; private set; }

        public Pipe(int x, int gapY)
        {
            X = x;
            GapY = gapY;
        }

        public void MoveLeft()
        {
            X--;
        }

        public void Render(int height, int pipeGapHeight, int width)
        {
            int x = X - GameConfig.PipeWidth / 2;
            for (int y = 0; y < height; y++)
            {
                if (x > 0 && x < width - 1 && (y < GapY || y > GapY + pipeGapHeight))
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write('█');
                }
            }
        }

        public void Erase(int height)
        {
            int x = X + GameConfig.PipeWidth / 2;
            if (x >= 0 && x < GameConfig.Width)
            {
                for (int y = 0; y < height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                }
            }
        }

        public bool IsColliding(Bird bird, int pipeWidth, int pipeGapHeight)
        {
            return Math.Abs(X - bird.X) < pipeWidth / 2 + 3 && ((int)bird.Y < GapY || (int)bird.Y > GapY + pipeGapHeight);
        }
    }
}