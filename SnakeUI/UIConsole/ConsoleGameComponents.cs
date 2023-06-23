namespace casnake.SnakeUI;

public class ConsoleGameComponents : IGameComponentsUI
{
    public string SnakeHead { get; } = "@";
    public string SnakeBody { get; } = "0";
    public string Background { get; } = " ";
    public string Fruit { get; } = "#";
    public string BorderMap { get; } = "*";


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