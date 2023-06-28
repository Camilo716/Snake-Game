namespace casnake.Tests;

using casnake.CasnakeGame.Moves;
using casnake.CasnakeGame.Trackers;
using casnake.Game;
using casnake.SnakeUI;

public class SnakeMovesTests
{
    SnakeUIStub _UIStub = new SnakeUIStub();

    [Test]
    public void validate_move_to_the_right()
    {
        var mover = CreateMover("right");
        var map = new SnakeMap(5,8);
        map.createInitialMap();

        var updatedMap = mover.MoveSnake(map);

        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0@#  *\n"+
            "*      *\n"+
            "********\n";
        var ExpectedBidimensionalMap = ConvertStringMapToBidimensionalMap(mapExpected);
        Assert.That(updatedMap.map, Is.EqualTo(ExpectedBidimensionalMap));
    }

    private SnakeMover CreateMover(string direction)
    {
        Coord headCoord = new Coord(2, 2); 
        Coord tailCoord = new Coord(1, 2); 

        var factory = new MoverFactory(headCoord, tailCoord);

        var mover = factory.CreateMover(direction);
        
        return mover;
    }   

    private string[,] ConvertStringMapToBidimensionalMap(string mapToConvert)
    {
        string[] lines = mapToConvert.Split("\n");
        int rows = lines.Length -1;
        int columns = lines[0].Length;


        string[,] map = new string[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                map[x, y] = lines[x][y].ToString();
            }
        }

        return map;   
    }
}