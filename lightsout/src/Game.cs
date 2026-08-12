using SDL3;

namespace LightsOut;

public class Game
{
    public IntPtr Window, Renderer;
    public bool Running = true;

    public IntPtr TextureOn, TextureOff;
    public Board Board = new Board();

    public Game()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("Lights Out Puzzle", 800, 600, 0, out Window, out Renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
        TextureOn = Image.LoadTexture(Renderer, "asset/on.png");
        TextureOff = Image.LoadTexture(Renderer, "asset/off.png");
    }

    public void Run()
    {
        Board = new Board();    
    }

    public void Loop()
    {
        while (SDL.PollEvent(out var e))
        {
            if ((SDL.EventType)e.Type == SDL.EventType.Quit)
            {
                Running = false;
            }
            if ((SDL.EventType)e.Type == SDL.EventType.MouseButtonUp)
            {
                uint button = e.Button.Button;
                float x = e.Button.X;
                float y = e.Button.Y;
                if (button == SDL.ButtonLeft)
                {
                    int col = (int)((x - Board.Left) / 80);
                    int row = (int)((y - Board.Top) / 80);
                    Board.Flip(row, col);
                }
            }
        }

        SDL.SetRenderDrawColor(Renderer, 255, 255, 127, 255);
        SDL.RenderClear(Renderer);
        Board.Render(this);
        SDL.RenderPresent(Renderer);
    }

    public void Dispose()
    {
        SDL.DestroyTexture(TextureOn);
        SDL.DestroyTexture(TextureOff);
        SDL.DestroyRenderer(Renderer);
        SDL.DestroyWindow(Window);
        SDL.Quit();
    }
}
