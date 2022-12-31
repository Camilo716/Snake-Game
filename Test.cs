namespace casnake;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void validate_creation_of_initial_map()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(3, 5);

        // 0****
        // **#**
        // *****

        game.createInitialMap();

        Assert.AreEqual("#", game.map[1,2]);
        }
    }

    [Test]
    public void validate_writing_of_map()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(3, 5);

        game.createInitialMap();
        game.writeMap();

        string mapExpected = "\n\n\n*****\n0@#**\n*****\n";
        // *****
        // 0@#**
        // *****
        Assert.AreEqual(mapExpected, ConsoleOutput.GetOuput());
        }
    }

    [Test]
    public void validate_move_right()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(4, 7);

        game.createInitialMap();
        game.moveRight();
        game.writeMap();
        
        string mapExpected = "\n\n\n\n*******\n*******\n*0@#***\n*******\n";
        // *******
        // *******
        // *0@#***
        // *******
        Assert.AreEqual(mapExpected, ConsoleOutput.GetOuput());
        }
    }
    [Test]
    public void validate_move_up()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(4, 7);

        game.createInitialMap();
        game.moveUp();
        game.writeMap();
        
        string mapExpected = "\n\n\n\n*******\n*@*****\n*0*#***\n*******\n";
        // *******
        // *@*****
        // *0*#***
        // *******
        Assert.AreEqual(mapExpected, ConsoleOutput.GetOuput());
        }
    }

    [Test]
    public void validate_finish_game()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(3, 5);


        game.finishGame();

        Assert.AreEqual("YOU CRASHED!\nScore: 0\n", ConsoleOutput.GetOuput());
        }
    }
}
