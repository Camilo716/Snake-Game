using casnake.CasnakeGame.Moves;
using casnake.CasnakeGame.Trackers;
using casnake.Game;
using casnake.SnakeUI;

namespace Tests;

public class SnakeMovesTests
{
    const string MoveToRightExpected = 
            "********\n"+
            "*      *\n"+
            "* 0@#  *\n"+
            "*      *\n"+
            "********\n";
    const string MoveUpExpected = 
            "********\n"+
            "* @    *\n"+
            "* 0 #  *\n"+
            "*      *\n"+
            "********\n";
    const string MoveDownExpected =
            "********\n"+
            "*      *\n"+
            "* 0 #  *\n"+
            "* @    *\n"+
            "********\n";


    [Theory]
    [InlineData("right", MoveToRightExpected)]
    [InlineData("down", MoveDownExpected)]
    [InlineData("up", MoveUpExpected)]
    public void MoveSnakeTest(string direction, string mapExpected)
    {
        var mover = CreateMover(direction, new Coord(2, 2), new Coord(1, 2));
        var map = new SnakeMap(5,8);
        map.createInitialMap();

        var updatedMap = mover.MoveSnake(map);

        var ExpectedBidimensionalMap = ConvertStringMapToBidimensionalMap(mapExpected);
        Assert.Equal(updatedMap.map, ExpectedBidimensionalMap);
    }

    [Fact]
    public void EatFruitTest()
    {
        var mover1 = CreateMover("right", new Coord(2, 2), new Coord(1, 2));
        var mover2 = CreateMover("right", new Coord(3, 2), new Coord(2, 2));
        var map = new SnakeMap(5,8);
        map.createInitialMap();

        var updatedMap = mover1.MoveSnake(map);
        updatedMap = mover2.MoveSnake(map);

        var (headCount, bodyCount, borderMapCount, fruitCount) = CountCurrentGameComponents(updatedMap.map);
        int perimeter = 2*(5+8) - 4;
        Assert.Equal(1, headCount);
        Assert.Equal(2, bodyCount);
        Assert.Equal(perimeter, borderMapCount);
        Assert.Equal(1, fruitCount);
    }

    private (int headCounter, int bodyCounter,  int borderMapCounter, int fruitCounter) CountCurrentGameComponents(string[,] map)
    {
        int headCounter = 0;
        int bodyCounter = 0;
        int borderMapCounter = 0;
        int fruitCounter = 0;

        IGameComponentsUI gameComponents = new ConsoleGameComponents();

        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                string currentComponent = map[row,column];
                bool isHead = currentComponent == gameComponents.SnakeHead;
                bool isBody = currentComponent == gameComponents.SnakeBody;
                bool isBorderMap = currentComponent == gameComponents.BorderMap;
                bool isFruit = currentComponent == gameComponents.Fruit;

                if (isHead)
                    headCounter++;
                if (isBody)
                    bodyCounter++;
                if (isBorderMap)
                    borderMapCounter++;
                if (isFruit)
                    fruitCounter++;
            }
        }
        return (headCounter, bodyCounter, borderMapCounter, fruitCounter);
    }

    private SnakeMover CreateMover(string direction, Coord headCoord, Coord tailCoord)
    {
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