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
        SnakeGame game = aSnakeGame(5, 8);

        // ________
        // |......|
        // |0@.#..|
        // |......|
        // ________

        game.createInitialMap();

        //Assert.AreEqual("#", game.map[2,4]);
        Assert.That("#", Is.EqualTo(game.map[2, 4]));
        }
    }

    [Test]
    public void validate_writing_of_map()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = aSnakeGame(5, 8);

        game.createInitialMap();
        game.writeMap();

        string mapExpected =
            "________\n"+
            "|......|\n"+
            "|0@.#..|\n"+
            "|......|\n"+
            "________\n";
        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }

    [Test]
    public void validate_move_right()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = aSnakeGame(5, 8);

        game.createInitialMap();
        game.moveRight();
        game.writeMap();
        
        string mapExpected =
        "________\n"+
        "|......|\n"+
        "|.0@#..|\n"+
        "|......|\n"+
        "________\n";

        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }
    [Test]
    public void validate_move_up()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = aSnakeGame(5, 8);

        game.createInitialMap();
        game.moveUp();
        game.writeMap();
        
        string mapExpected =
        "________\n"+
        "|.@....|\n"+
        "|.0.#..|\n"+
        "|......|\n"+
        "________\n";

        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }

    [Test]
    public void validate_finish_game()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = aSnakeGame(3, 5);


        game.finishGame();

        string finishMessage = 
        "-------------\n"+
        "YOU CRASHED!\n"+
        "Score: 0\n"+
        "-------------\n";

        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(finishMessage)); 
        }
    }


    private SnakeGame aSnakeGame(int heightOfMap, int widthOfMap)
    {
        var math = new SnakeMath();
        var userInterface = new SnakeUI();
        var game = new SnakeGame(heightOfMap, widthOfMap, math, userInterface);

        return game;
    }
}
