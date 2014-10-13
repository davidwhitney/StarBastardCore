using System;

namespace StarBastard.Windows
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new StarBastard())
            {
                game.Run();
            }
        }
    }
}
