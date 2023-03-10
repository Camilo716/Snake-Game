namespace casnake.Game;
using casnake.SnakeUI;

public class SnakeGame
{
    public SnakeMap _snakeMap;
    public SnakeTracker _tracker;
    private string actualMove = "right";
    private int score = 2;
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

            bool validMovement = checkIfWasAValidMovement();

            if (validMovement == false)
            {
                _tracker.removeInvalidMovementFromRegistry();
                continue;
            }

            setActualMove();

            if (verificateIfPlayerCrashed())
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

    private bool checkIfWasAValidMovement()
    {
        if (_tracker.getLastMove() == "error")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void setActualMove()
    {
        actualMove = _tracker.getLastMove();
    }

    private bool verificateIfPlayerCrashed()
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
        bool fruitAhead = checkIfAheadThereIsAFruit();

        moveSnakeHead(actualMove);
        moveSnakeBody(fruitAhead);
    }

    public bool checkIfAheadThereIsAFruit()
    {
        if (_snakeMap.whatIsAhead(actualMove, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit")
        {
            return true;
        }
        return false;
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
            score+=1;
        }
        else
        {
            _snakeMap.modifyActualCeil(_tracker.tailTrackerY, _tracker.tailTrackerX, background);
            _tracker.trackTailSnake(score); 
        }
    }

    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {score}\n-------------");
    }
}
