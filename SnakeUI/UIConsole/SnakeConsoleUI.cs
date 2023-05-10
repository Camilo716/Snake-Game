namespace casnake.SnakeUI;

public class SnakeUIConsole : ISnakeUI
{
    private IGameComponentsUI _consoleGameComponents;
    private readonly string _snakeHead;
    private readonly string _snakeBody;
    private readonly string _fruit;


    public SnakeUIConsole()
    {
        _consoleGameComponents = new ConsoleGameComponents();
        _snakeHead = _consoleGameComponents.getGameComponent("SnakeHead");
        _snakeBody = _consoleGameComponents.getGameComponent("SnakeBody");
        _fruit = _consoleGameComponents.getGameComponent("Fruit");

    }   

    public void writeMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
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
                bool isSnake = map[row,column] == _snakeHead || map[row,column] == _snakeBody;
                bool isFruit =  map[row,column] == _fruit;

                if (isSnake)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write((map[row,column]));
                }
                else if (isFruit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((map[row,column]));        
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write((map[row,column]));
                }
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