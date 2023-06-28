using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverError : SnakeMover
{
    protected override Coord headCeilAheadCoords => throw new NotImplementedException();
    public MoverError(Coord headCoords, Coord tailCoords) : base(headCoords, tailCoords) { }

}