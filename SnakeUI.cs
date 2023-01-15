public interface SnakeUI
{
    public void writeCharInGame(string charToWrite);
    public void writeMessage(string message);
    public string readNextMove();
}

public class SnakeUIConsole : SnakeUI
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
}
