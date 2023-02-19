namespace casnake.SnakeUI;
public interface ISnakeUI
{
    public void writeMessage(string message);
    public string readNextMove();
    public string drawGame(string [,] map);
}

public interface IGameComponentsUI
{
    public string getGameComponent(string gameComponent);
}

