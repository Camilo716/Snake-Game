namespace casnake.CasnakeGame.Trackers;

public static class TrackerFactory
{
    public static ITrackeable CreateTracker(string direction)
    {
        switch (direction)
        {
            case "up":
                return new TrackerUp();
                
            case "down":
                return new TrackerDown();
            
            case "right":
                return new TrackerRight();
            
            case "left":
                return new TrackerLeft();
                 
            default:
                return new TrackerUp();
        }  
    }
}