namespace casnake.Game;

public class SnakeTracker
{
    public int headTracker_row;
    public int headTracker_column;
    public int tailTracker_row;
    public int tailTracker_column;
    
    public void trackHeadSnake(int headRow, int headColumn)
    {
        headTracker_row = headRow;
        headTracker_column = headColumn;
    }

    public void trackTailSnake(string tailDirection)
    {
        switch (tailDirection)
        {
            case "up":
                tailTracker_row = tailTracker_row-1;
                break;

            case "down":
                tailTracker_row = tailTracker_row+1;
                break;

            case "right":
                tailTracker_column = tailTracker_column+1;
                break;

            case "left":
                tailTracker_column = tailTracker_column-1;
                break;     

            default:
                break;
       }
    }

    public void trackSnakeForInitialMap(string[,] map, string head)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                if (map[row,column] == head)
                {
                    trackHeadSnake(row,column);
                    tailTracker_row = row;
                    tailTracker_column = column-1;
                }
            }
        }
    }


    public string controlTrack()
    {
        return $"Head:\t{headTracker_row}, {headTracker_column}\nTail:\t{tailTracker_row}, {tailTracker_column}";
    }
}
