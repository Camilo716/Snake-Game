namespace casnake.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void validate_creation_of_initial_map()
    {
        SnakeGame game = CreateSnakeGame(5, 8);

        // ********
        // *      *
        // *0@ #  *
        // *      *
        // ********

        game.createInitialMap();

        Assert.That("#", Is.EqualTo(game.map[2, 4]));
        Assert.That("@", Is.EqualTo(game.map[2,2]));
    }

    [Test]
    public void validate_move_up()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game.createInitialMap();
        game.moveUp();        
        
        string mapExpected =
            "********\n"+
            "* @    *\n"+
            "* 0 #  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(UIStub.viewMap(game.map) , Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_Down()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game.createInitialMap();
        game.moveDown();        
        
        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0 #  *\n"+
            "* @    *\n"+
            "********\n";
        Assert.That(UIStub.viewMap(game.map) , Is.EqualTo(mapExpected));
    }

    [Test]
    public void validate_move_to_the_right()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game.createInitialMap();
        game.moveRight();

        string mapExpected =
            "********\n"+
            "*      *\n"+
            "* 0@#  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(UIStub.viewMap(game.map), Is.EqualTo(mapExpected));

    }

    [Test]
    public void validate_move_to_the_left()
    {
        SnakeGame game = CreateSnakeGame(5, 8);
        SnakeUIStub UIStub = CreateSnakeUIStub();

        game.createInitialMap();
        game.moveUp();
        game.moveLeft();

        string mapExpected =
            "********\n"+
            "*@0    *\n"+
            "*   #  *\n"+
            "*      *\n"+
            "********\n";
        Assert.That(UIStub.viewMap(game.map), Is.EqualTo(mapExpected));

    }


    private SnakeGame CreateSnakeGame(int heightOfMap, int widthOfMap)
    {
        var math = new SnakeMath();
        var userInterface = new SnakeUIConsole();
        var game = new SnakeGame(heightOfMap, widthOfMap, math, userInterface);

        return game;
    }

    private SnakeUIStub CreateSnakeUIStub()
    {
        var UIStub = new SnakeUIStub();
        return UIStub;
    }
}
