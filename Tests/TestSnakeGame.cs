namespace casnake.Tests;
using casnake.Game;
using casnake.SnakeUI;

public class Tests
{
    SnakeUIStub _UIStub = new SnakeUIStub();
    IGameComponentsUI _gameComponents = new ConsoleGameComponents();
    string fruit = "";
    string snakeBody = "";
    string snakeHead = "";
    string borderMap = "";
    string background = "";

    [SetUp]
    public void createGameComponents()
    {
        fruit = _gameComponents.getGameComponent("Fruit");
        snakeBody = _gameComponents.getGameComponent("SnakeBody");
        snakeHead = _gameComponents.getGameComponent("SnakeHead");
        borderMap = _gameComponents.getGameComponent("BorderMap");
        background = _gameComponents.getGameComponent("Background");
    }

    [Test]
    public void validate_move_up()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        
        game._tracker.registMove("up");
        game.setActualMove();
        game.moveSnake();        

        string mapExpected =
            "********\n"+
            "* @    *\n"+
            "* 0 #  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(_UIStub.drawGame(game._snakeMap.map) , Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_Down()
    {
        SnakeGame game = CreateSnakeGame(5, 8);

        game._tracker.registMove("down");
        game.setActualMove();
        game.moveSnake();        
        
        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0 #  *\n"+
            "* @    *\n"+
            "********\n";
        Assert.That(_UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_to_the_right()
    {
        SnakeGame game = CreateSnakeGame(5, 8);

        game._tracker.registMove("right");
        game.setActualMove();
        game.moveSnake();

        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0@#  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(_UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_to_the_left()
    {
        SnakeGame game = CreateSnakeGame(5, 8);

        game._tracker.registMove("up");
        game.setActualMove();
        game.moveSnake();
        game._tracker.registMove("left");
        game.setActualMove();
        game.moveSnake();

        string mapExpected =
            "********\n"+
            "*@0    *\n"+
            "*   #  *\n"+
            "*      *\n"+
            "********\n";
        
        Assert.That(_UIStub.drawGame(game._snakeMap.map), Is.EqualTo(mapExpected));
    }
    
    private SnakeGame CreateSnakeGame(int heightOfMap , int widthOfMap)
    {
        SnakeUIStub userInterface = new SnakeUIStub();
        SnakeMap SMap = new SnakeMap(heightOfMap, widthOfMap);

        var game = new SnakeGame(userInterface, SMap);

        game._snakeMap.createInitialMap();
        game._tracker.trackSnakeForInitialMap(game._snakeMap.map, snakeHead);
        game._tracker.registMove("right");

        return game;
    }
}
