namespace casnake;

public class SnakeGame
{
    private string tailDirection = "right";
    private int score = 1;
    private bool playerLose = false;
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

        while (playerLose == false)
        {
            string moveToDo = _userInterface.readNextMove();
            nextMove(moveToDo);
            _userInterface.writeMessage(_tracker.controlTrack());
        }
        
        finishGame();
    }

    private void startGame()
    {
        _snakeMap.createInitialMap();
        _userInterface.drawGame(_snakeMap.map);
        _tracker.trackSnakeForInitialMap(_snakeMap.map, _entities.snakeHead);
        _userInterface.writeMessage(_tracker.controlTrack());
    }

    private void nextMove(string moveToDo)
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
        bool playerCrashed = _tracker.headTracker_row == 1 || _snakeMap.map[_tracker.headTracker_row-1, _tracker.headTracker_column] == _entities.snakeBody;
        bool aheadThereIsAFruit = _snakeMap.map[_tracker.headTracker_row-1, _tracker.headTracker_column] == _entities.fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

            _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column] = _entities.snakeBody;
            _snakeMap.map[_tracker.headTracker_row-1, _tracker.headTracker_column] = _entities.snakeHead;
            _tracker.trackHeadSnake(_tracker.headTracker_row-1, _tracker.headTracker_column);

        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTracker_row, _tracker.tailTracker_column] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);        
        }
    }

    public void moveDown()
    {
        bool playerCrashed = _tracker.headTracker_row == _snakeMap.map.GetLength(0)-2 || _snakeMap.map[_tracker.headTracker_row+1, _tracker.headTracker_column] == _entities.snakeBody;
        bool aheadThereIsAFruit = _snakeMap.map[_tracker.headTracker_row+1, _tracker.headTracker_column] == _entities.fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

            _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column] = _entities.snakeBody;
            _snakeMap.map[_tracker.headTracker_row+1, _tracker.headTracker_column] = _entities.snakeHead;
            _tracker.trackHeadSnake(_tracker.headTracker_row+1, _tracker.headTracker_column);

        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTracker_row, _tracker.tailTracker_column] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);           
        }
    }
        
    public void moveRight()
    {
        bool playerCrashed = _tracker.headTracker_column == _snakeMap.map.GetLength(1)-2 || _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column+1] == _entities.snakeBody;
        bool aheadThereIsAFruit = _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column+1] == _entities.fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

        _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column] = _entities.snakeBody;
        _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column+1] = _entities.snakeHead;
        _tracker.trackHeadSnake(_tracker.headTracker_row, _tracker.headTracker_column+1);

        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTracker_row, _tracker.tailTracker_column] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);
        }
    }

    public void moveLeft()
    {
        bool playerCrashed = _tracker.headTracker_column == 1|| _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column-1] == _entities.snakeBody;
        bool aheadThereIsAFruit = _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column-1] == _entities.fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

            _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column] = _entities.snakeBody;
            _snakeMap.map[_tracker.headTracker_row, _tracker.headTracker_column-1] = _entities.snakeHead;
            _tracker.trackHeadSnake(_tracker.headTracker_row, _tracker.headTracker_column-1);

        if (aheadThereIsAFruit)
        {
            _snakeMap.generateFruit();
            score+=1;
        }
        else
        {
            _snakeMap.map[_tracker.tailTracker_row, _tracker.tailTracker_column] = _entities.backgroundMap;
            trackTailDirection();
            _tracker.trackTailSnake(tailDirection);
        }
    }

    public void trackTailDirection()
    {
        string upToTail = _snakeMap.map[_tracker.tailTracker_row-1,_tracker.tailTracker_column];
        string downToTail = _snakeMap.map[_tracker.tailTracker_row+1,_tracker.tailTracker_column];
        string rightToTail = _snakeMap.map[_tracker.tailTracker_row,_tracker.tailTracker_column+1];
        string leftToTail = _snakeMap.map[_tracker.tailTracker_row,_tracker.tailTracker_column-1];
        
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
    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {score}\n-------------");
    }

}
