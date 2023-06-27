using casnake.CasnakeGame.Trackers;

namespace casnake.Game;

public class SnakeTracker
{
    public Coord headCoord;
    public Coord tailCoord;

    public int headTrackerY { get; private set; }
    public int headTrackerX { get; private set; }
    public int tailTrackerY { get; private set; }
    public int tailTrackerX { get; private set; }
    private List<string> moveRegister;
 
    public SnakeTracker()
    {
        this.moveRegister = new List<string>();
    }
    
    public void registMove(string moveToRegist)
    {
        moveRegister.Add(moveToRegist);
    }

    public void removeInvalidMovementFromRegistry()
    {
        moveRegister.RemoveAt(moveRegister.Count - 1);
    }

    public string getLastMove()
    {
        return moveRegister.Last();
    }

    public void trackSnakeForInitialMap(string[,] map, string head)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                if (map[row,column] == head)
                {
                    headTrackerY = row;
                    headTrackerX = column;
                    tailTrackerY = row;
                    tailTrackerX = column-1;
                }
            }
        }
    }

    public void TrackSnakeForInitialMap(string[,] map, string head)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                if (map[row,column] == head)
                {
                    this.headCoord = new Coord(column, row);
                    this.tailCoord = new Coord(column-1, row);
                }
            }
        }
    }

    public void trackHead(string direction)
    {
        var pointTracker = TrackerFactory.CreateTracker(direction);
        pointTracker.TrackMove(ref headCoord);
    }

    public void trackTail(int bodyLength)
    {
        var direction = trackTailDirection(bodyLength);
        var pointTracker = TrackerFactory.CreateTracker(direction);
        pointTracker.TrackMove(ref headCoord);
    }

    public void trackHeadSnake(string direction,int headRow, int headColumn)
    {
        switch (direction)
        {
            case "up":
                headTrackerY = headTrackerY-1;
                break;
            case "down":
                headTrackerY = headTrackerY+1;
                break;
            case "right":
                headTrackerX = headTrackerX+1;
                break;
            case "left":
                headTrackerX = headTrackerX-1;
                break;     
            default:
                break;
        }  
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
    }

    private string trackTailDirection(int bodyLength)
    {
        return moveRegister[moveRegister.Count()-bodyLength];
    }

    private string controlTrack(string tileDirection, int score)
    {
        return
            "Head:\t" + headTrackerY + ", " + headTrackerX+ "\n" +
            "Tail:\t" + tailTrackerY + ", " + tailTrackerX + "\n" +
            "TailDirectionToMove:" + tileDirection + "\n" +
            "BodyLenght:" + score;
    }
}