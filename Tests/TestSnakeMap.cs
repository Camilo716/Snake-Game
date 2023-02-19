namespace casnake.Tests;
using casnake.Game;
using casnake.SnakeUI;

public class TestSnakeMap
{
    IGameComponentsUI _gameComponents = new ConsoleGameComponents();
    string? fruit;
    string? snakeBody;
    string? snakeHead;
    string? borderMap;
    string? background;

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
    public void validate_creation_of_initial_map()
    {
        SnakeMap SMap = createMap();

        // ********
        // *      *
        // *0@ #  *
        // *      *
        // ********

        SMap.createInitialMap();

        Assert.That(fruit, Is.EqualTo(SMap.map[2, 4]));
        Assert.That(snakeHead, Is.EqualTo(SMap.map[2,2]));
    }

    public SnakeMap createMap()
    {
        SnakeMap SMap = new SnakeMap(5,8);
        return SMap;
    }
}
