namespace casnake.Tests;
using casnake.Game;
using casnake.SnakeUI;

public class SnakeGameTests
{
    SnakeUIStub _UIStub = new SnakeUIStub();
    

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

    private SnakeGame CreateSnakeGame(int heightOfMap , int widthOfMap)
    {
        SnakeUIStub userInterface = new SnakeUIStub();
        SnakeMap SMap = new SnakeMap(heightOfMap, widthOfMap);
        IGameComponentsUI _gameComponents = new ConsoleGameComponents();

        var game = new SnakeGame(userInterface, SMap);

        game._snakeMap.createInitialMap();
        game._tracker.TrackSnakeForInitialMap(game._snakeMap.map, _gameComponents.SnakeHead);
        game._tracker.registMove("right");

        return game;
    }
}
