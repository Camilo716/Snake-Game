namespace casnake.Tests;

public class TestSnakeMap
{
    [SetUp]
    public void Setup()
    {
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

        Assert.That(SMap._entities.fruit, Is.EqualTo(SMap.map[2, 4]));
        Assert.That(SMap._entities.snakeHead, Is.EqualTo(SMap.map[2,2]));
    }

    public SnakeMap createMap()
    {
        SnakeMap SMap = new SnakeMap(5,8);
        return SMap;
    }
}
