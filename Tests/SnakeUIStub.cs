namespace casnake.Tests;

public class SnakeUIStub : SnakeUI
{
    public void writeCharInGame(string charToWrite)
    {
        Console.Write(charToWrite);
    }

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
    
    public string viewMap(string[,] map)
    {
        string actualMap = "";

        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int column = 0; column < map.GetLength(1); column++)
            {
                //_userInterface.writeCharInGame(map[row,column]);
                actualMap += map[row,column];
            }
            //_userInterface.writeCharInGame("\n");
            actualMap += "\n";
        }
        return actualMap;
    }
}
