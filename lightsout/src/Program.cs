using SDL3;

namespace LightsOut;

internal static class Program
{
    private static void Main()
    {
        Game game = new Game();
        game.Run();
        while (game.Running)
        {
            game.Loop();
        }
        game.Dispose();
    }
}
