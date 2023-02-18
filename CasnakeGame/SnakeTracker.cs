namespace casnake.Game;

public class SnakeTracker
{
    public int headTrackerY;
    public int headTrackerX;
    public int tailTrackerY;
    public int tailTrackerX;
    public int snakeLarge;
    private List<string> moveRegister;
 
    public SnakeTracker()
    {
        this.moveRegister = new List<string>();
    }
    
    public void registMove(string moveToRegist)
    {
        moveRegister.Add(moveToRegist);
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

    public void trackHeadSnake(int headRow, int headColumn)
    {
        headTrackerY = headRow;
        headTrackerX = headColumn;
    }

    public void trackTailSnake(int bodyLength)
    {
        string tailDirectionToMove = trackTailDirection(bodyLength);

        switch (tailDirectionToMove)
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
        controlTrack(tailDirectionToMove, bodyLength);
    }

    private string trackTailDirection(int bodyLength)
    {
        return moveRegister[moveRegister.Count()-bodyLength];
    }

    private void controlTrack(string tileDirection, int score)
    {
        
        Console.WriteLine(
            "Head:\t" + headTrackerY + ", " + headTrackerX+ "\n" +
            "Tail:\t" + tailTrackerY + ", " + tailTrackerX + "\n" +
            "TailDirectionToMove:" + tileDirection + "\n" +
            "BodyLenght:" + score);
    }
}
