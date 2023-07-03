namespace casnake.Tests;
using casnake.Game;
using casnake.SnakeUI;

public class TestSnakeMap
{
    IGameComponentsUI _gameComponents = new ConsoleGameComponents();

    [Test]
    public void InitialMapCreationTest()
    {
        SnakeMap SMap = new SnakeMap(5,8);

        // ********
        // *      *
        // *0@ #  *
        // *      *
        // ********

        SMap.createInitialMap();

        Assert.That(_gameComponents.Fruit, Is.EqualTo(SMap.map[2, 4]));
        Assert.That(_gameComponents.SnakeHead, Is.EqualTo(SMap.map[2,2]));
    }
}
