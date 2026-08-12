using SDL3;

namespace LightsOut;

public class Board
{
    public int Row = 7;
    public int Col = 7;
    public int Left = 0, Top = 0;
    public List<List<int>> Cell = new List<List<int>>();
    public int[,] Neighbor = new int[,] {
        {-1, 0}, {0, -1}, {0, 0}, {0, 1}, {1, 0}
    };

    public Board()
    {
        Row = 7;
        Col = 7;
        Cell.Clear();

        for (int i = 0; i < Row; i++)
        {
            List<int> temp = new List<int>();
            for (int j = 0; j < Col; j++)
            {
                temp.Add(0);
            }
            Cell.Add(temp);
        }

        Left = 400 - Row * 40;
        Top = 40;

        for (int i = 0; i < 8; i++)
        {
            int index = Random.Shared.Next(Row * Col);
            int col = index % Col;
            int row = (index - col) / Col;
            Flip(row, col);
        }
    }

    public bool IsInside(int row, int col)
    {
        return row >= 0 && row < Row && col >= 0 && col < Col;
    }

    public void Flip(int row, int col)
    {
        for (int i = 0; i < 5; i++)
        {
            int rTarget = row + Neighbor[i,0];
            int cTarget = col + Neighbor[i,1];
            if (IsInside(rTarget, cTarget))
            {
                Cell[rTarget][cTarget] = 1 - Cell[rTarget][cTarget];
            }
        }
    }

    public void Render(Game game)
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                SDL.FRect rect = new SDL.FRect{X = Left + 80 * j, Y = Top + 80 * i, W = 80, H = 80};
                if (Cell[i][j] == 0)
                {
                    SDL.RenderTexture(game.Renderer, game.TextureOff, IntPtr.Zero, rect);
                }
                else
                {
                    SDL.RenderTexture(game.Renderer, game.TextureOn, IntPtr.Zero, rect);
                }
            }
        }
    }
}
