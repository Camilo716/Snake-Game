public class SnakeGame
{
    // Counters
    private int snakeBodyHorizontalCounter = 1;
    private int snakeBodyVerticalCounter = 0;
    private int score = 1;
    //Trackers
    private int headTracker_r;
    private int headTracker_c;
    private int tailTracker_r;
    private int tailTracker_c;
    private string tailDirection;
    // Entities
    private string snakeHead = "@";
    private string snakeBody = "0";
    private string backgroundMap = " ";
    private string fruit = "#";
    private string borderOfMap = "*";
    // Game controlers
    private bool playerLose = false;
    public int heightOfMap; // Rows
    public int widthOfMap;  // Columns
    public string[,] map;
    private int maxIndexRow;
    private int maxIndexColumn;
    // Dependences
    private SnakeMath _math;
    private SnakeUI _userInterface;

    public SnakeGame(int heightOfMap, int widthOfMap, SnakeMath _math, SnakeUI _userInterface)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
        this._math = _math;
        this._userInterface = _userInterface;
    }

    public void playGame()
    {
        this.createInitialMap();
        this.writeMap();

        while (this.playerLose == false)
        {
            string moveToDo = _userInterface.readNextMove();
            nextMove(moveToDo);
        }
        
        finishGame();
    }

    public void createInitialMap()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                // The loop is in:
                bool place_to_create_snake = row == _math.round(maxIndexRow/2) && column == 2;
                bool middle_of_map = row == _math.round(maxIndexRow/2) && column == _math.round(maxIndexColumn/2);
                bool vertical_border_of_map = row == 0 || row == maxIndexRow-1;
                bool horizonal_border_of_map = column == 0 || column == maxIndexColumn-1;

                if (place_to_create_snake)
                {
                    this.map[row,column] = snakeHead;
                    this.map[row, column-1] = snakeBody;

                    headTracker_c = column;
                    headTracker_r = row;
                    tailTracker_r = row;
                    tailTracker_c = column-1;
                }
                else if (middle_of_map)
                {
                    this.map[row,column] = fruit;
                }
                else if (vertical_border_of_map)
                {
                    this.map[row,column] = borderOfMap;
                }
                else if (horizonal_border_of_map)
                {
                    this.map[row,column] = borderOfMap;
                }
                else{
                    this.map[row,column] = backgroundMap;
                }
            }
        }
        tailDirection = "rigth";
    }

    private void nextMove(string moveToDo)
    {
        switch (moveToDo)
        {
            case "w":
                moveUp();
                writeMap();
                break;
            case "a":
                moveLeft();
                writeMap();
                break;
            case "s":
                moveDown();
                writeMap();
                break;
            case "d":
                moveRight();
                writeMap();
                break;
            default:
                break;
        }
    }

    public void writeMap()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                _userInterface.writeCharInGame(map[row,column]);
            }
            _userInterface.writeCharInGame("\n");
        }
    }

    public void moveUp()
    {
        bool playerCrashed = headTracker_c == 1 || map[headTracker_r-1, headTracker_c] == snakeBody;
        bool aheadThereIsAFruit = map[headTracker_r-1, headTracker_c] == fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }
        if (aheadThereIsAFruit)
        {
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r-1, headTracker_c] = snakeHead;

            trackHeadSnake(headTracker_r-1, headTracker_c);
            generateFruit();
        }
        else
        {
            map[tailTracker_r, tailTracker_c] = backgroundMap;
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r-1, headTracker_c] = snakeHead;

            trackHeadSnake(headTracker_r-1, headTracker_c);
            trackTailSnake();        
        }
    }

    public void moveDown()
    {
        bool playerCrashed = headTracker_r == maxIndexRow-2 || map[headTracker_r+1, headTracker_c] == snakeBody;
        bool aheadThereIsAFruit = map[headTracker_r+1, headTracker_c] == fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }
        if (aheadThereIsAFruit)
        {
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r+1, headTracker_c] = snakeHead;

            trackHeadSnake(headTracker_r+1, headTracker_c);
            generateFruit();
        }
        else
        {
            map[tailTracker_r, tailTracker_c] = backgroundMap;
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r+1, headTracker_c] = snakeHead;

            trackHeadSnake(headTracker_r+1, headTracker_c);
            trackTailSnake();            
        }

    }
        
    public void moveRight()
    {
        bool playerCrashed = headTracker_c == maxIndexColumn-2 || map[headTracker_r, headTracker_c+1] == snakeBody;
        bool aheadThereIsAFruit = map[headTracker_r, headTracker_c+1] == fruit;


        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

        if (aheadThereIsAFruit)
        {
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r, headTracker_c+1] = snakeHead;

            trackHeadSnake(headTracker_r, headTracker_c+1);
            generateFruit();
        }
        else
        {
            map[tailTracker_r, tailTracker_c] = backgroundMap;
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r, headTracker_c+1] = snakeHead;

            trackHeadSnake(headTracker_r, headTracker_c+1);
            trackTailSnake();
        }
    }

    public void moveLeft()
    {
        bool playerCrashed = headTracker_c == 1|| map[headTracker_r, headTracker_c-1] == snakeBody;
        bool aheadThereIsAFruit = map[headTracker_r, headTracker_c-1] == fruit;

        if (playerCrashed)
        {
            playerLose = true;
            return;
        }

        if (aheadThereIsAFruit)
        {
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r, headTracker_c-1] = snakeHead;

            trackHeadSnake(headTracker_r, headTracker_c-1);
            generateFruit();
        }
        else
        {
            map[tailTracker_r, tailTracker_c] = backgroundMap;
            map[headTracker_r, headTracker_c] = snakeBody;
            map[headTracker_r, headTracker_c-1] = snakeHead;

            trackHeadSnake(headTracker_r, headTracker_c-1);
            trackTailSnake();
        }
    }

    private void trackHeadSnake(int headRow, int headColumn)
    {
        headTracker_r = headRow;
        headTracker_c = headColumn;
    }

    private void trackTailSnake()
    {
        verificateTailDirection();

        switch (tailDirection)
        {
            case "up":
                tailTracker_r = tailTracker_r-1;
                break;

            case "down":
                tailTracker_r = tailTracker_r+1;
                break;

            case "right":
                tailTracker_c = tailTracker_c+1;
                break;

            case "left":
                tailTracker_c = tailTracker_c-1;
                break;     

            default:
                break;
       }
    }

    private void verificateTailDirection()
    {
        string upToTail = map[tailTracker_r-1,tailTracker_c];
        string downToTail = map[tailTracker_r+1,tailTracker_c];
        string rightToTail = map[tailTracker_r,tailTracker_c+1];
        string leftToTail = map[tailTracker_r,tailTracker_c-1];
        
        if (upToTail == snakeBody)
        {
            tailDirection = "up";
        }
        if (downToTail == snakeBody)
        {
            tailDirection = "down";
        }    
        if (rightToTail == snakeBody)
        {
            tailDirection = "right";
        }
        if (leftToTail == snakeBody)
        {
            tailDirection =  "left";
        }
    }

    private void generateFruit()
    {

        while (true)
        {
            int rowNewFruit = _math.randomNumberForRow(maxIndexRow);
            int columnNewFruit = _math.randomNumberForColumn(maxIndexColumn);

            bool placeWithoutAnotherEntities = map[rowNewFruit, columnNewFruit] == " ";


            if (placeWithoutAnotherEntities)
            {
                map[rowNewFruit, columnNewFruit] = fruit;
                break;
            }
        }

        score +=1;
        
    }
    private void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {score}\n-------------");
    }

}