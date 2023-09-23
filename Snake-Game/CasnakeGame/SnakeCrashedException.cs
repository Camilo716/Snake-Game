namespace casnake.Game;

public class SnakeCrashedException : Exception
{
    public SnakeCrashedException(): base("Player Crashed") { }
}