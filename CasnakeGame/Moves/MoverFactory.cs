using casnake.CasnakeGame.Trackers;

namespace casnake.CasnakeGame.Moves;

public class MoverFactory
{
    private Coord _headCoords;
    private Coord _tailCoords;

    public MoverFactory(Coord headCoords, Coord tailCoords)
    {
        _headCoords = headCoords;
        _tailCoords = tailCoords;
    }

    public SnakeMover CreateMover(string move)
    {
        switch (move)
        {
            case "up":
                return new MoverUp(_headCoords, _tailCoords);

            case "down":
                return new MoverDown(_headCoords, _tailCoords);

            case "right":
                return new MoverRight(_headCoords, _tailCoords);

            case "left":
                return new MoverLeft(_headCoords, _tailCoords);

            default:
                return new MoverRight(_headCoords, _tailCoords);
       }      
    }
}