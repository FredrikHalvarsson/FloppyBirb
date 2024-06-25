using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloppyBirb.Helpers
{
    public static class GameConfig
    {
        public const float Gravity = .5f;
        public const int PipeWidth = 8;
        public const int PipeGapHeight = 6;
        public const int SpaceBetweenPipes = 45;
        public const string BirdUp = @"-(v'}";
        public const string BirdDown = @"-(^'}";
        public static int Width = 120;
        public static int Height = 30;
    }
}
