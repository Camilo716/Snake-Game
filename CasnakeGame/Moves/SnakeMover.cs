using casnake.CasnakeGame;
using casnake.CasnakeGame.Trackers;
using casnake.Game;
using casnake.SnakeUI;

namespace casnake.CasnakeGame.Moves;

public abstract class SnakeMover
{
    private IGameComponentsUI gameComponents = new ConsoleGameComponents();    
    protected abstract Coord headCeilAheadCoords { get; }

    protected Coord _headCoords;
    protected Coord _tailCoords;

    public SnakeMover(Coord headCoords, Coord tailCoords)
    {
        _headCoords = headCoords;
        _tailCoords = tailCoords;
    }

    public SnakeMap MoveSnake(SnakeMap gameMap)
    {
        var fruitAhead = FruitAhead(gameMap);

        gameMap = MoveHead(gameMap);

        if (fruitAhead)
        {
            gameMap.generateFruit();
        }
        else
        {
            gameMap = MoveBody(gameMap);
        }

        return  gameMap;
    }
    
    public bool SnakeCrashed(SnakeMap gameMap)
    {
        bool playerCrashed = gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] == gameComponents.BorderMap;
        return playerCrashed;
    }

    private SnakeMap MoveHead(SnakeMap gameMap)
    {
        gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] = gameComponents.SnakeHead;
        gameMap.map[_headCoords.Y, _headCoords.X] = gameComponents.SnakeBody;
        return gameMap;
    }

    private SnakeMap MoveBody(SnakeMap gameMap)
    {
        gameMap.map[_tailCoords.Y, _tailCoords.X] = gameComponents.Background;   
        gameMap.map[_headCoords.Y, _headCoords.X] = gameComponents.SnakeBody;
        
        return gameMap;
    }

    private bool FruitAhead(SnakeMap gameMap)
    {
        var ceilAhead = gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X];
        return ceilAhead == gameComponents.Fruit;
    }
}