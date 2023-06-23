namespace casnake.Tests;
using casnake.Game;
using casnake.SnakeUI;

public class TestSnakeMap
{
    IGameComponentsUI _gameComponents = new ConsoleGameComponents();

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

        Assert.That(_gameComponents.Fruit, Is.EqualTo(SMap.map[2, 4]));
        Assert.That(_gameComponents.SnakeHead, Is.EqualTo(SMap.map[2,2]));
    }

    public SnakeMap createMap()
    {
        SnakeMap SMap = new SnakeMap(5,8);
        return SMap;
    }
}
