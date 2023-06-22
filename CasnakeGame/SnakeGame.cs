namespace casnake.Game;
using casnake.SnakeUI;

public class SnakeGame
{
    public SnakeMap _snakeMap;
    public SnakeTracker _tracker;
    private string actualMove = "right";
    private int snakeLenght = 2;
    private ISnakeUI _userInterface;

    private IGameComponentsUI  _gameComponents;
    private string snakeBody;
    private string snakeHead;
    private string background;
    private string fruit;
    private string borderMap;

    public SnakeGame(ISnakeUI _userInterface, SnakeMap _snakeMap)
    {
        this._userInterface = _userInterface;
        this._snakeMap = _snakeMap;
        this._tracker = new SnakeTracker();

        _gameComponents = new ConsoleGameComponents();
        this.snakeHead = _gameComponents.getGameComponent("SnakeHead");
        this.snakeBody = _gameComponents.getGameComponent("SnakeBody");
        this.fruit = _gameComponents.getGameComponent("Fruit");
        this.borderMap = _gameComponents.getGameComponent("BorderMap");
        this.background = _gameComponents.getGameComponent("Background");
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
    
    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.trackSnakeForInitialMap(_snakeMap.map, snakeHead);
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
        bool validMove = !(_tracker.getLastMove() == "error");
        
        return validMove;
    }

    public void setActualMove()
    {
        actualMove = _tracker.getLastMove();
    }

    private bool PlayerCrashed()
    {        
        bool playerCrashed = _snakeMap.whatIsAhead(actualMove, _tracker.headTrackerY, _tracker.headTrackerX) == "collition";

        if (playerCrashed)
        {
            return true;
        }
        return false;
    }

    public void moveSnake()
    {        
        bool fruitAhead = aheadThereIsAFruit();

        moveSnakeHead(actualMove);
        moveSnakeBody(fruitAhead);
    }

    public bool aheadThereIsAFruit()
    {        
        bool aheadThereIsAFruit = _snakeMap.whatIsAhead(actualMove, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";
        return aheadThereIsAFruit;
    }

    public void moveSnakeHead(string direction)
    {
        _snakeMap.modifyActualCeil(_tracker.headTrackerY, _tracker.headTrackerX, snakeBody);
        _snakeMap.modifyCeilAhead(actualMove, _tracker.headTrackerY, _tracker.headTrackerX, snakeHead);
        _tracker.trackHeadSnake(actualMove, _tracker.headTrackerY, _tracker.headTrackerX);
    }


    public void moveSnakeBody(bool aheadThereIsAFruit)
    {
        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            snakeLenght+=1;
        }
        else
        {
            _snakeMap.modifyActualCeil(_tracker.tailTrackerY, _tracker.tailTrackerX, background);
            _tracker.trackTailSnake(snakeLenght); 
        }
    }

    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {snakeLenght}\n-------------");
    }
}
