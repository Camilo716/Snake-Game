namespace casnake.SnakeUI;

public class ConsoleGameComponents : IGameComponentsUI
{
    public string SnakeHead { get; } = "@";
    public string SnakeBody { get; } = "0";
    public string Background { get; } = " ";
    public string Fruit { get; } = "#";
    public string BorderMap { get; } = "*";
}