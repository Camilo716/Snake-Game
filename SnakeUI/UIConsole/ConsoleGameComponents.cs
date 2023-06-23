namespace casnake.SnakeUI;

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