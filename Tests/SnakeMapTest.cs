using casnake.Game;
using casnake.SnakeUI;

namespace Tests;

public class TestSnakeMap
{
    IGameComponentsUI _gameComponents = new ConsoleGameComponents();

    [Fact]
    public void InitialMapCreationTest()
    {
        SnakeMap SMap = new SnakeMap(5,8);

        // ********
        // *      *
        // *0@ #  *
        // *      *
        // ********

        SMap.createInitialMap();

        Assert.Equal(_gameComponents.Fruit, SMap.map[2, 4]);
        Assert.Equal(_gameComponents.SnakeHead, SMap.map[2,2]);
    }
}