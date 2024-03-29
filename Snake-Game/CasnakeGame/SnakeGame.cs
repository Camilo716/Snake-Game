namespace casnake.Game;

using casnake.CasnakeGame.Moves;
using casnake.CasnakeGame.Trackers;
using casnake.SnakeUI;

public class SnakeGame
{
    private SnakeMap _snakeMap;
    private SnakeTracker _tracker;
    private int _snakeLenght = 2;
    private ISnakeUI _userInterface;
    private IGameComponentsUI  _gameComponents;

    public SnakeGame(ISnakeUI _userInterface, SnakeMap _snakeMap)
    {
        this._userInterface = _userInterface;
        this._snakeMap = _snakeMap;
        this._tracker = new SnakeTracker();

        _gameComponents = new ConsoleGameComponents();
    }

    public void playGame()
    {
        startGame();
        IterateGame();
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {_snakeLenght}\n-------------");
    }

    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.TrackSnakeForInitialMap(_snakeMap.map, _gameComponents.SnakeHead);
        _tracker.registMove("right");
    }

    private void IterateGame()
    {
        while (true)
        {
            string moveToDo = _userInterface.readNextMove();
            registNextMove(moveToDo);
            if (!WasAValidMovement())
            {
                _tracker.removeInvalidMovementFromRegistry();
                continue;
            }

            SnakeMover mover = CreateMover();
            if (mover.SnakeCrashed(_snakeMap))
            {
                return;
            }
            MakeMove(mover);

            _userInterface.drawGame(_snakeMap.map);
        }  
    }

    private void registNextMove(string moveToDo)
    {        
        switch (moveToDo)
        {
            case "w":
                _tracker.registMove("up");
                break;
            case "a":
                _tracker.registMove("left");
                break;
            case "s":
                _tracker.registMove("down");
                break;
            case "d":
                _tracker.registMove("right");
                break;
            default:
                _tracker.registMove("error");
                break;
        }
    }

    private SnakeMover CreateMover()
    {
        var factory = new MoverFactory(_tracker.headCoord, _tracker.tailCoord);
        SnakeMover mover = factory.CreateMover(_tracker.getLastMove());
        return mover;
    }

    private void MakeMove(SnakeMover mover)
    {
        _snakeMap = mover.MoveSnake(_snakeMap);
        TraceMadeMove();
    }

    private void TraceMadeMove()
    {
        _tracker.trackHead(_tracker.getLastMove());

        if (snakeAte(_snakeMap.map))
        {
            _snakeLenght++;
        }
        else
        {
            _tracker.trackTail(_snakeLenght);
        }
    }

    private bool snakeAte(string[,] map)
    {
        int lenghtCounter = 1;
        lenghtCounter += map
                            .Cast<String>()
                            .Count(ceil => ceil == "0");

        return lenghtCounter > _snakeLenght;
    }

    private bool WasAValidMovement()
    {
        return  _tracker.getLastMove() != "error";
    }
}
