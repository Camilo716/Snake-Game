using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverError : SnakeMover
{
    public override Coord headCeilAheadCoords => throw new NotImplementedException();

    public override Coord tailCeilCoords => throw new NotImplementedException();

    public void MoveSnake()
    {
        throw new NotImplementedException();
    }
}