namespace casnake.Game;
using casnake.SnakeUI;

public class SnakeGame
{
    public SnakeMap _snakeMap;
    public SnakeTracker _tracker;
    private string moveToDo;
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
        this.moveToDo = "right";

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
            moveToDo = _userInterface.readNextMove();
        
            if (verificateIfPlayerCrashed(moveToDo))
            {
                finishGame();
                return;
            }
            nextMove();
        }  
    }

    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.trackSnakeForInitialMap(_snakeMap.map, snakeHead);
        _tracker.registMove("right");
    }

    public bool verificateIfPlayerCrashed(string direction)
    {
        bool playerCrashed = _snakeMap.whatIsAhead(direction, _tracker.headTrackerY, _tracker.headTrackerX) == "collition";

        if (playerCrashed)
        {
            return true;
        }
        return false;
    }

    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {score}\n-------------");
    }

    private void nextMove()
    {
        switch (moveToDo)
        {
            case "w":
                _tracker.registMove("up");
                moveUp();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "a":
                _tracker.registMove("left");
                moveLeft();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "s":
                _tracker.registMove("down");
                moveDown();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "d":
                _tracker.registMove("right");
                moveRight();
                _userInterface.drawGame(_snakeMap.map);
                break;
            default:
                break;
        }
    }

    public void moveUp()
    {
        bool fruitAhead = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = snakeBody;
        _snakeMap.map[_tracker.headTrackerY-1, _tracker.headTrackerX] = snakeHead;
    
        _tracker.trackHeadSnake(_tracker.headTrackerY-1, _tracker.headTrackerX);

        if (fruitAhead)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = background;
            _tracker.trackTailSnake(score); 
        }

    }

    public void moveDown()
    {
        bool fruitAhead = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = snakeBody;
        _snakeMap.map[_tracker.headTrackerY+1, _tracker.headTrackerX] = snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY+1, _tracker.headTrackerX);

        if (fruitAhead)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = background;
            _tracker.trackTailSnake(score);      
        }
    }
        
    public void moveRight()
    {
        bool fruitAhead = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = snakeBody;
        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX+1] = snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY, _tracker.headTrackerX+1);

        if (fruitAhead)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = background;
            _tracker.trackTailSnake(score);       
        }
    }

    public void moveLeft()
    {
        bool fruitAhead = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = snakeBody;
        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX-1] = snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY, _tracker.headTrackerX-1);

        if (fruitAhead)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = background;
            _tracker.trackTailSnake(score);
        }
    }
}
