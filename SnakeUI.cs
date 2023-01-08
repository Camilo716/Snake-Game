public class SnakeUI
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
        string nextMove = Console.ReadLine();
        return nextMove;
    }
}
