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
        SnakeGame game = new SnakeGame(5, 8);

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
        SnakeGame game = new SnakeGame(5, 8);

        game.createInitialMap();
        game.writeMap();

        string mapExpected = "\n\n\n\n\n________\n|......|\n|0@.#..|\n|......|\n________\n";
        // ________
        // |......|
        // |0@.#..|
        // |......|
        // ________
        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }

    [Test]
    public void validate_move_right()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(5, 8);

        game.createInitialMap();
        game.moveRight();
        game.writeMap();
        
        string mapExpected = "\n\n\n\n\n________\n|......|\n|.0@#..|\n|......|\n________\n";
        // ________
        // |......|
        // |.0@#..|
        // |......|
        // ________

        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }
    [Test]
    public void validate_move_up()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(5, 8);

        game.createInitialMap();
        game.moveUp();
        game.writeMap();
        
        string mapExpected = "\n\n\n\n\n________\n|.@....|\n|.0.#..|\n|......|\n________\n";
        // ________
        // |.@....|
        // |.0.#..|
        // |......|
        // ________
        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(mapExpected));
        }
    }

    [Test]
    public void validate_finish_game()
    {
        using (var ConsoleOutput = new ConsoleOutput())
        {
        SnakeGame game = new SnakeGame(3, 5);


        game.finishGame();

        string finishMessage = "-------------\nYOU CRASHED!\nScore: 0\n-------------\n";

        Assert.That(ConsoleOutput.GetOuput(), Is.EqualTo(finishMessage)); 
        }
    }
}
