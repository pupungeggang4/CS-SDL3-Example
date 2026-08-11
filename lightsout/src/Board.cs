using SDL3;

namespace LightsOut;

public class Board
{
    public int Row = 7;
    public int Col = 7;
    public List<List<int>> Cell;
    public int[5][2] Neighbor = {
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
    }

    public bool IsInside(int row, int col)
    {
        return row >= 0 && row < Row && col >= 0 && col < Col;
    }

    public void Flip(int row, int col)
    {
        for (int i = 0; i < 5; i++)
        {
            int rTarget = row + Neighbor[i][0];
            int cTarget = col + Neighbor[i][1];
            if (IsInside(rTarget, cTarget))
            {
                Cell[rTarget][cTarget] = 1 - Cell[rTarget][cTarget];
            }
        }
    }
}
