public interface ISnakeUI
{
    public void writeMessage(string message);
    public string readNextMove();
    public string drawGame(string [,] map);

}

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
