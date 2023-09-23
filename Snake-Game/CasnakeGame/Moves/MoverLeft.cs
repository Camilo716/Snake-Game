using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverLeft : SnakeMover
{

    protected override Coord headCeilAheadCoords { get => new Coord(base._headCoords.X-1, base._headCoords.Y); }

    public MoverLeft(Coord headCoords, Coord tailCoords) : base(headCoords, tailCoords) { }
}