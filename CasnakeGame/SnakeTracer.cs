using casnake.CasnakeGame.Trackers;

namespace casnake.Game;

public class SnakeTracer
{
    public Coord headCoord;
    public Coord tailCoord;
    private List<string> moveRegister;
 
    public SnakeTracer()
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
        pointTracker.TrackMove(ref tailCoord);
    }

    private string trackTailDirection(int bodyLength)
    {
        return moveRegister[moveRegister.Count()-bodyLength];
    }
}