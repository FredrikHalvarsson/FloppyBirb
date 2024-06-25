using System;

namespace FloppyBirb.Helpers
{
    public static class Renderer
    {
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void SetCursorVisible(bool isVisible)
        {
            Console.CursorVisible = isVisible;
        }

        public static void SetWindowSize(int width, int height)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = width;
                Console.WindowHeight = height;
            }
        }

        public static void ResetWindowSize(int originalWidth, int originalHeight)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = originalWidth;
                Console.WindowHeight = originalHeight;
            }
        }
    }
}