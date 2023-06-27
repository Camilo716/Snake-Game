using casnake.CasnakeGame;
using casnake.CasnakeGame.Trackers;
using casnake.Game;
using casnake.SnakeUI;

namespace casnake.CasnakeGame.Moves;

public abstract class SnakeMover
{
    private IGameComponentsUI gameComponents =  new ConsoleGameComponents();    
    public abstract Coord headCeilAheadCoords { get; }
    public abstract Coord tailCeilCoords { get; }

    public SnakeMap MoveSnake(SnakeMap gameMap)
    {
        MoveHead(gameMap);
        MoveTail(gameMap);

        return gameMap;
    }

    private SnakeMap MoveHead(SnakeMap gameMap)
    {
        
        gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] = gameComponents.SnakeHead;

        return gameMap;
    }

    private SnakeMap MoveTail(SnakeMap gameMap)
    {
        if (!FruitAhead(gameMap))
            gameMap.map[tailCeilCoords.Y, tailCeilCoords.X] = gameComponents.Background;

        return gameMap;
    }

    private bool FruitAhead(SnakeMap gameMap)
    {
        return gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] == gameComponents.Fruit;
    }
}