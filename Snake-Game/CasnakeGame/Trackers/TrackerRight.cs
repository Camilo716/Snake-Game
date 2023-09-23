namespace casnake.CasnakeGame.Trackers;

public class TrackerRight : ITrackeable
{
    public void TrackMove(ref Coord coord)
    {
        coord.X++;
    }
}