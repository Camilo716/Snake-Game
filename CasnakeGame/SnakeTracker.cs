namespace casnake.Game;

public class SnakeTracker
{
    public int headTrackerY;
    public int headTrackerX;
    public int tailTrackerY;
    public int tailTrackerX;
    
    public void trackHeadSnake(int headRow, int headColumn)
    {
        headTrackerY = headRow;
        headTrackerX = headColumn;
    }


    public void trackTailSnake(string tailDirection)
    {
        switch (tailDirection)
        {
            case "up":
                tailTrackerY = tailTrackerY-1;
                break;

            case "down":
                tailTrackerY = tailTrackerY+1;
                break;

            case "right":
                tailTrackerX = tailTrackerX+1;
                break;

            case "left":
                tailTrackerX = tailTrackerX-1;
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
                    tailTrackerY = row;
                    tailTrackerX = column-1;
                }
            }
        }
    }


    public string controlTrack(string tileDirection)
    {
        return $"Head:\t{headTrackerY}, {headTrackerX}\nTail:\t{tailTrackerY}, {tailTrackerX} {tileDirection}";
        
    }
}
