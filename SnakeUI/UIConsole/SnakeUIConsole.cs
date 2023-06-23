namespace casnake.SnakeUI;

public class SnakeUIConsole : ISnakeUI
{
    private IGameComponentsUI _gameComponents;

    public SnakeUIConsole()
    {
        _gameComponents = new ConsoleGameComponents();
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
                bool isSnake = map[row,column] == _gameComponents.SnakeHead || map[row,column] == _gameComponents.SnakeBody;
                bool isFruit =  map[row,column] == _gameComponents.Fruit;

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