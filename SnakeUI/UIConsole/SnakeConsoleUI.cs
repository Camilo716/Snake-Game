namespace casnake.SnakeUI;

public class SnakeUIConsole : ISnakeUI
{
    public void writeMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string readNextMove()
    {
        string? nextMove = Console.ReadLine();
        if (nextMove != null)
        {
            return nextMove;
        }
        return "";
    }

    public string drawGame(string[,] map)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                Console.Write((map[row,column]));
            }
            Console.Write("\n");
        }
        return "";
    }
}
public class ConsoleGameComponents : IGameComponentsUI
{
    Dictionary<string, string> gameComponents = new Dictionary<string, string>()
    {
        {"SnakeHead", "@"},
        {"SnakeBody", "0"},
        {"Background", " "},
        {"Fruit", "#"},
        {"BorderMap", "*"}
    };

    public string getGameComponent(string gameComponent)
    {
        if (gameComponents.ContainsKey(gameComponent))
        {
            return gameComponents[gameComponent];
        }

        return " ";
    }
}