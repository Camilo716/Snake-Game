using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverRight : SnakeMover
{
    public override Coord headCeilAheadCoords { get => new Coord(this._headCoords.X, this._headCoords.Y - 1);}

    public override Coord tailCeilCoords { get => new Coord(this._tailCoords.X, this._tailCoords.Y); }

    private Coord _headCoords;
    private Coord _tailCoords;

    public MoverRight(Coord headCoords, Coord tailCoords)
    {
        _headCoords = headCoords;
        _tailCoords = tailCoords;
    }
}