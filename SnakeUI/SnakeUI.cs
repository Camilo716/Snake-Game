namespace casnake.SnakeUI;
public interface ISnakeUI
{
    public void writeMessage(string message);
    public string readNextMove();
    public string drawGame(string [,] map);
}

public interface IGameComponentsUI
{
    public string SnakeHead { get; }
    public string SnakeBody { get; }
    public string Background { get; }
    public string Fruit { get; }
    public string BorderMap { get; }
    public string getGameComponent(string gameComponent);
}

