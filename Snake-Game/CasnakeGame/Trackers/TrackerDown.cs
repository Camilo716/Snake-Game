namespace casnake.CasnakeGame.Trackers;

public class TrackerDown : ITrackeable
{
    public void TrackMove(ref Coord coord)
    {
        coord.Y++;
    }
}