namespace casnake.Tests;
using casnake.Game;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void validate_move_up()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();
        
        game._tracker.registMove("up");
        game.moveUp();        

        string mapExpected =
            "********\n"+
            "* @    *\n"+
            "* 0 #  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(UIStub.drawGame(game._snakeMap.map) , Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_Down()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game._tracker.registMove("down");
        game.moveDown();        
        
        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0 #  *\n"+
            "* @    *\n"+
            "********\n";
        Assert.That(UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_to_the_right()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game._tracker.registMove("right");
        game.moveRight();

        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0@#  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_to_the_left()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game._tracker.registMove("up");
        game.moveUp();
        game._tracker.registMove("left");
        game.moveLeft();

        string mapExpected =
            "********\n"+
            "*@0    *\n"+
            "*   #  *\n"+
            "*      *\n"+
            "********\n";
        
        Assert.That(UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }
    
    private SnakeGame CreateSnakeGame(int heightOfMap , int widthOfMap)
    {
        SnakeUIStub userInterface = new SnakeUIStub();
        SnakeMap SMap = new SnakeMap(heightOfMap, widthOfMap);

        var game = new SnakeGame(userInterface, SMap);

        game._snakeMap.createInitialMap();
        game._tracker.trackSnakeForInitialMap(game._snakeMap.map, game._entities.snakeHead);
        game._tracker.registMove("right");

        return game;
    }

    private SnakeUIStub CreateSnakeUIStub()
    {
        var UIStub = new SnakeUIStub();
        return UIStub;
    }
}
