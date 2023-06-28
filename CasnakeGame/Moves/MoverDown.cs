using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverDown : SnakeMover
{
    protected override Coord headCeilAheadCoords { get => new Coord(base._headCoords.X, base._headCoords.Y+1); }

    public MoverDown(Coord headCoords, Coord tailCoords) : base(headCoords, tailCoords) { } 
}