namespace casnake.CasnakeGame.Trackers;

public class TrackerLeft : ITrackeable
{
    public void TrackMove(ref Coord coord)
    {
        coord.X--;
    }
}