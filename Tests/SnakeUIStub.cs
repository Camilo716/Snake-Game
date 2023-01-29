namespace casnake.Tests;

public class SnakeUIStub : ISnakeUI
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
        string actualMap = "";

        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                actualMap += map[row,column];
            }
            actualMap += "\n";
        }
        return actualMap;
    }
}
