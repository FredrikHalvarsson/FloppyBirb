using System;
using FloppyBirb.Entities;
using FloppyBirb.Helpers;

namespace FloppyBirb.Factories
{
    // Factory Method Pattern: PipeFactory class encapsulates the creation 
    // of Pipe objects, allowing for dynamic creation based on parameters.
    public static class PipeFactory
    {
        // CreatePipe method creates a new Pipe object with random gap position.
        // It utilizes the Factory Method pattern to decouple object creation 
        public static Pipe CreatePipe(int x, int height)
        {
            // Randomly generate gap position within valid bounds.
            int gapY = Random.Shared.Next(0, height - GameConfig.PipeGapHeight - 1 - 6) + 3;

            // Create and return a new Pipe object with the specified parameters.
            return new Pipe(x, gapY);
        }
    }
}