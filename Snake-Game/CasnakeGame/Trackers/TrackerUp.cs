namespace casnake.CasnakeGame.Trackers;

public class TrackerUp : ITrackeable
{
    public void TrackMove(ref Coord coord)
    {
        coord.Y--;
    }
}