using System;
using System.Collections.Generic;
using System.Threading;
using FloppyBirb.Entities;
using FloppyBirb.Factories;
using FloppyBirb.Strategies;
using FloppyBirb.Helpers;

namespace FloppyBirb.Game
{
    // Singleton Pattern: Game class represents the main game logic and is
    // instantiated only once during the application's lifecycle.
    public class Game
    {
        private static Game _instance;
        private readonly int OriginalWidth = Console.WindowWidth;
        private readonly int OriginalHeight = Console.WindowHeight;
        private readonly TimeSpan Sleep = TimeSpan.FromMilliseconds(90);
        private List<Pipe> Pipes = new();
        private Bird Bird;
        private int Frame;
        private int PipeFrame;

        // Singleton Classes can easily be identified by having private constructors
        private Game() { }

        // Singleton Pattern: Instance property ensures only one instance of Game exists.
        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Game();
                }
                return _instance;
            }
        }

        public void Start()
        {
            try
            {
            PlayAgain:
                Renderer.ClearConsole();
                Pipes.Clear();
                if (OperatingSystem.IsWindows())
                {
                    GameConfig.Width = 120;
                    GameConfig.Height = 30;
                    Renderer.SetWindowSize(GameConfig.Width, GameConfig.Height);
                }
                else
                {
                    GameConfig.Width = Console.WindowWidth;
                    GameConfig.Height = Console.WindowHeight;
                }

                // Creating a Bird with the NormalMovement Strategy pattern
                Bird = new Bird(GameConfig.Width / 6, GameConfig.Height / 2, new NormalMovement());

                Frame = 0;
                PipeFrame = GameConfig.SpaceBetweenPipes;
                Renderer.SetCursorVisible(false);
                // Starting Input
                Bird.Render(GameConfig.BirdUp, GameConfig.BirdDown);
                Console.SetCursorPosition((int)Bird.X - 10, (int)Bird.Y + 1);
                Console.Write("Press Space To Flap");

            StartingInput:
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Spacebar:
                        Bird.Flap(); // Bird using Flap from the Movement Strategy Pattern
                        break;
                    case ConsoleKey.Escape:
                        Renderer.ClearConsole();
                        Console.Write("Floppy Birb was closed.");
                        return;
                    default:
                        goto StartingInput;
                }
                Console.SetCursorPosition((int)Bird.X - 10, (int)Bird.Y + 1);
                Console.Write("                   ");
                // Game Loop
                while (true)
                {
                    // Check For Game Over
                    if (Console.WindowHeight != GameConfig.Height || Console.WindowWidth != GameConfig.Width)
                    {
                        Renderer.ClearConsole();
                        Console.Write("You resized the console. Floppy Birb was closed.");
                        return;
                    }
                    if (Frame == int.MaxValue)
                    {
                        Console.SetCursorPosition(0, GameConfig.Height - 1);
                        Console.Write("You win! Score: " + Frame + ".");
                        break;
                    }
                    if (!(Bird.Y < GameConfig.Height - 1 && Bird.Y > 0) || IsBirdCollidingWithPipe())
                    {
                        Console.SetCursorPosition(0, GameConfig.Height - 1);
                        Console.Write("Game Over. Score: " + Frame + ".");
                        Console.Write(" Play Again [enter], or quit [escape]?");
                    GetPlayAgainInput:
                        ConsoleKey key = Console.ReadKey(true).Key;
                        if (key is ConsoleKey.Enter)
                        {
                            goto PlayAgain;
                        }
                        else if (key is not ConsoleKey.Escape)
                        {
                            goto GetPlayAgainInput;
                        }
                        Renderer.ClearConsole();
                        break;
                    }

                    // Capture input within the game loop to ensure spacebar is registered
                    while (Console.KeyAvailable)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.Spacebar:
                                Bird.Flap();
                                break;
                            case ConsoleKey.Escape:
                                Renderer.ClearConsole();
                                Console.Write("Flappy Bird was closed.");
                                return;
                        }
                    }

                    // Updates
                    {
                        // Pipes
                        {
                            // Erase (previous frame)
                            foreach (var pipe in Pipes)
                            {
                                pipe.Erase(GameConfig.Height);
                            }
                            // Update
                            for (int i = 0; i < Pipes.Count; i++)
                            {
                                Pipes[i].MoveLeft();
                            }
                            if (Pipes.Count > 0 && Pipes[0].X < -GameConfig.PipeWidth)
                            {
                                Pipes.RemoveAt(0);
                            }
                            // Factory Pattern: PipeFactory ensures correct creation of the Pipe model instances
                            if (PipeFrame >= GameConfig.SpaceBetweenPipes)
                            {
                                Pipes.Add(PipeFactory.CreatePipe(GameConfig.Width + GameConfig.PipeWidth / 2, GameConfig.Height));
                                PipeFrame = 0;
                            }
                            // Render (current frame)
                            foreach (var pipe in Pipes)
                            {
                                pipe.Render(GameConfig.Height, GameConfig.PipeGapHeight, GameConfig.Width);
                            }
                            Bird.Render(GameConfig.BirdUp, GameConfig.BirdDown);
                            PipeFrame++;
                        }
                        // Bird
                        {
                            // Erase (previous frame)
                            Bird.Erase();
                            // Update
                            Bird.Update();
                            // Render (current frame)
                            Bird.Render(GameConfig.BirdUp, GameConfig.BirdDown);
                        }
                        Frame++;
                    }
                    Thread.Sleep(Sleep);
                }
            }
            finally
            {
                Renderer.SetCursorVisible(true);
                Renderer.ResetWindowSize(OriginalWidth, OriginalHeight);
            }
        }

        private bool IsBirdCollidingWithPipe()
        {
            foreach (var pipe in Pipes)
            {
                if (pipe.IsColliding(Bird, GameConfig.PipeWidth, GameConfig.PipeGapHeight))
                {
                    return true;
                }
            }
            return false;
        }
    }
}