namespace casnake.Game;

public class SnakeGame
{
    private string tailDirection = "right";
    private int score = 1;
    private string moveToDo;
    private ISnakeUI _userInterface;
    public SnakeMap _snakeMap;
    public Entities _entities;
    public SnakeTracker _tracker;
    

    public SnakeGame(ISnakeUI _userInterface, SnakeMap _snakeMap)
    {
        this._userInterface = _userInterface;
        this._snakeMap = _snakeMap;
        this._entities = new Entities();
        this._tracker = new SnakeTracker();
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
            _userInterface.writeMessage(_tracker.controlTrack(tailDirection));
        }  
    }

    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.trackSnakeForInitialMap(_snakeMap.map, _entities.snakeHead);
        _userInterface.writeMessage(_tracker.controlTrack(tailDirection));
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
                moveUp();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "a":
                moveLeft();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "s":
                moveDown();
                _userInterface.drawGame(_snakeMap.map);
                break;
            case "d":
                moveRight();
                _userInterface.drawGame(_snakeMap.map);
                break;
            default:
                break;
        }
    }

    public void moveUp()
    {
        bool eateble = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = _entities.snakeBody;
        _snakeMap.map[_tracker.headTrackerY-1, _tracker.headTrackerX] = _entities.snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY-1, _tracker.headTrackerX);

        if (eateble)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);       
        }
    }

    public void moveDown()
    {
        bool eateble = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = _entities.snakeBody;
        _snakeMap.map[_tracker.headTrackerY+1, _tracker.headTrackerX] = _entities.snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY+1, _tracker.headTrackerX);

        if (eateble)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);      
        }
    }
        
    public void moveRight()
    {
        bool eateble = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = _entities.snakeBody;
        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX+1] = _entities.snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY, _tracker.headTrackerX+1);

        if (eateble)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);        }
    }

    public void moveLeft()
    {
        bool eateble = _snakeMap.whatIsAhead(moveToDo, _tracker.headTrackerY, _tracker.headTrackerX) == "fruit";

        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX] = _entities.snakeBody;
        _snakeMap.map[_tracker.headTrackerY, _tracker.headTrackerX-1] = _entities.snakeHead;
        _tracker.trackHeadSnake(_tracker.headTrackerY, _tracker.headTrackerX-1);

        if (eateble)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTrackerY, _tracker.tailTrackerX] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);
        }
    }

    public void trackTailDirection()
    {

        string upToTail = _snakeMap.map[_tracker.tailTrackerY-1,_tracker.tailTrackerX];
        string downToTail = _snakeMap.map[_tracker.tailTrackerY+1,_tracker.tailTrackerX];
        string rightToTail = _snakeMap.map[_tracker.tailTrackerY,_tracker.tailTrackerX+1];
        string leftToTail = _snakeMap.map[_tracker.tailTrackerY,_tracker.tailTrackerX-1];

        // string upToTail = _snakeMap.setCeilAhead("up", _tracker.tailTrackerY, _tracker.tailTrackerX);
        // string downToTail = _snakeMap.setCeilAhead("down", _tracker.tailTrackerY, _tracker.tailTrackerX);
        // string rightToTail = _snakeMap.setCeilAhead("right", _tracker.tailTrackerY, _tracker.tailTrackerX);
        // string leftToTail = _snakeMap.setCeilAhead("left", _tracker.tailTrackerY, _tracker.tailTrackerX);
        
        if (upToTail == _entities.snakeBody)
        {
            tailDirection = "up";
        }
        else if (downToTail == _entities.snakeBody)
        {
            tailDirection = "down";
        }    
        else if (rightToTail == _entities.snakeBody)
        {
            tailDirection = "right";
        }
        else if (leftToTail == _entities.snakeBody)
        {
            tailDirection =  "left";
        }
    }
}
