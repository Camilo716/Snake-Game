using casnake.CasnakeGame;
using casnake.CasnakeGame.Trackers;
using casnake.Game;
using casnake.SnakeUI;

namespace casnake.CasnakeGame.Moves;

public abstract class SnakeMover
{
    private IGameComponentsUI gameComponents =  new ConsoleGameComponents();    
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
        SnakeMap withHeadUpdated = MoveHead(gameMap);
        SnakeMap withBodyUpdated = MoveBody(gameMap);

        return gameMap;
    }

    private SnakeMap MoveHead(SnakeMap gameMap)
    {
        gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] = gameComponents.SnakeHead;

        return gameMap;
    }

    private SnakeMap MoveBody(SnakeMap gameMap)
    {
        if (FruitAhead(gameMap))
        {
            gameMap.generateFruit();
        }
        else
        {
            gameMap.map[_tailCoords.Y, _tailCoords.X] = gameComponents.Background;   
            gameMap.map[_headCoords.Y, _headCoords.X] = gameComponents.SnakeBody;
        }
        return gameMap;
    }

    private bool FruitAhead(SnakeMap gameMap)
    {
        return gameMap.map[headCeilAheadCoords.Y, headCeilAheadCoords.X] == gameComponents.Fruit;
    }
}