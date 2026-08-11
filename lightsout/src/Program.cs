using SDL3;

namespace LightsOut;

internal static class Program
{
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("Lights Out Puzzle", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }

        SDL.SetRenderDrawColor(renderer, 0, 0, 0, 0);

        var loop = true;

        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
                if ((SDL.EventType)e.Type == SDL.EventType.Quit)
                {
                    loop = false;
                }
            }

            SDL.RenderClear(renderer);
            SDL.RenderPresent(renderer);
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);

        SDL.Quit();
    }
}
