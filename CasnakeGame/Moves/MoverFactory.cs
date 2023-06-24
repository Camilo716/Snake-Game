namespace casnake.CasnakeGame.Moves;

public class MoverFactory
{
    public IMovable CreateMover(string move)
    {
        switch (move)
        {
            case "up":
                return new MoverUp();

            case "down":
                return new MoverDown();

            case "right":
                return new MoverRight();

            case "left":
                return new MoverLeft();

            default:
                return new MoverError();
       }      
    }
}