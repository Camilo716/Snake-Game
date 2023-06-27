namespace casnake.Game;

using casnake.CasnakeGame.Moves;
using casnake.CasnakeGame.Trackers;
using casnake.SnakeUI;

public class SnakeGame
{
    public SnakeMap _snakeMap;
    public SnakeTracker _tracker;
    private string _actualMove = "right";
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

        while (true)
        {
            string moveToDo = _userInterface.readNextMove();
            registNextMove(moveToDo);

            if (!WasAValidMovement())
            {
                _tracker.removeInvalidMovementFromRegistry();
                continue;
            }

            setActualMove();

            if (PlayerCrashed())
            {
                finishGame();
                return;
            }
            
            moveSnake();
            _userInterface.drawGame(_snakeMap.map);
        }  
    }

    public void moveSnake()
    {
        bool fruitAhead = aheadThereIsAFruit();

        moveSnakeHead();
        moveSnakeBody(fruitAhead);
    }

    public void _moveSnake()
    {
        var headCoords = new Coord(_tracker.headTrackerX, _tracker.headTrackerY);
        var tailCoords = new Coord(_tracker.tailTrackerX, _tracker.tailTrackerY);

        SnakeMover mover = new MoverFactory(headCoords, tailCoords)
                                                .CreateMover(_actualMove);

        bool fruitAhead = aheadThereIsAFruit();

        moveSnakeHead();
        moveSnakeBody(fruitAhead);
    }
    
    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.TrackSnakeForInitialMap(_snakeMap.map, _gameComponents.SnakeHead);
        _tracker.registMove("right");
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

    private bool WasAValidMovement()
    {
        bool validMove = _tracker.getLastMove() != "error";
        
        return validMove;
    }

    public void setActualMove()
    {
        _actualMove = _tracker.getLastMove();
    }

    private bool PlayerCrashed()
    {        
        bool playerCrashed = _snakeMap.whatIsAhead(_actualMove, _tracker.headCoord.Y, _tracker.headCoord.X) == "collition";

        return playerCrashed;
    }


    private bool aheadThereIsAFruit()
    {        
        bool aheadThereIsAFruit = _snakeMap.whatIsAhead(_actualMove, _tracker.headCoord.Y, _tracker.headCoord.X) == "fruit";
        return aheadThereIsAFruit;
    }

    private void moveSnakeHead()
    {
        _snakeMap.modifyActualCeil(_tracker.headCoord.Y, _tracker.headCoord.X, _gameComponents.SnakeBody);
        _snakeMap.modifyCeilAhead(_actualMove, _tracker.headCoord.Y , _tracker.headCoord.X, _gameComponents.SnakeHead);
        
        _tracker.trackHead(_actualMove);
        // _tracker.trackHeadSnake(_actualMove, _tracker.headTrackerY, _tracker.headTrackerX);
    }


    private void moveSnakeBody(bool aheadThereIsAFruit)
    {
        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            _snakeLenght+=1;
        }
        else
        {
            _snakeMap.modifyActualCeil(_tracker.tailCoord.Y, _tracker.tailCoord.X, _gameComponents.Background);
            _tracker.trackTail(_snakeLenght);
            // _tracker.trackTailSnake(_snakeLenght); 
        }
    }

    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {_snakeLenght}\n-------------");
    }
}
