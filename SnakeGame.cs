public class SnakeGame
{
    // Counters
    public int snakeBodyHorizontalCounter = 1;
    public int snakeBodyVerticalCounter = 0;
    public int score = 0;
    // Entities
    public string snakeHead = "@";
    public string snakeBody = "0";
    public string backgroundMap = ".";
    public string fruit = "#";
    public string horizontalBorder = "_";
    public string verticalBorder = "|";
    // Game controlers
    public bool playerLose = false;
    public int heightOfMap; // Rows
    public int widthOfMap;  // Columns
    public string[,] map;
    public string? moveToDo;
    public int maxIndexRow;
    public int maxIndexColumn;
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
            this.moveToDo = _userInterface.readNextMove();
            nextMove();
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
                bool place_to_create_snake = row == _math.Round(maxIndexRow/2) && column == 2;
                bool middle_of_map           = row == _math.Round(maxIndexRow/2) && column == _math.Round(maxIndexColumn/2);
                bool vertical_border_of_map  = row == 0 || row == maxIndexRow-1;
                bool horizonal_border_of_map = column == 0 || column == maxIndexColumn-1; 

                if (place_to_create_snake)
                {
                    this.map[row,column] = snakeHead;
                    this.map[row, column-1] = snakeBody;
                }
                else if (middle_of_map)
                {
                    this.map[row,column] = fruit;
                }
                else if (vertical_border_of_map)
                {
                    this.map[row,column] = horizontalBorder;
                }
                else if (horizonal_border_of_map)
                {
                    this.map[row,column] = verticalBorder;
                }
                else{
                    this.map[row,column] = backgroundMap;
                }
            }
        }
    }

    public void nextMove()
    {
        switch (this.moveToDo)
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
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                if (map[row,column] == snakeHead)
                {
                    bool player_crashed_with_borders   = map[row,column] == map[1, column];
                    bool player_crashed_with_snakebody = map[row-1,column] == snakeBody;
                    bool snake_is_going_to_the_right   = map[row-1,column] == backgroundMap &&  map[row+1,column] != snakeBody && map[row,column+1] != snakeBody;
                    bool snake_is_going_to_the_left    = map[row-1,column] == backgroundMap &&  map[row+1,column] != snakeBody && map[row,column-1] != snakeBody;
                    bool snake_is_going_up             = map[row-1,column] == backgroundMap && map[row+1, column] == snakeBody;

                    if (player_crashed_with_borders)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (player_crashed_with_snakebody)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (snake_is_going_to_the_right)
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row,column-snakeBodyHorizontalCounter] = backgroundMap;

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (snake_is_going_to_the_left)
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row,column+snakeBodyHorizontalCounter] = backgroundMap;

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (snake_is_going_up)
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row+snakeBodyVerticalCounter, column] = backgroundMap;
                        return;
                    }                  
                }
            }
        }
    }

    public void moveDown()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                if (map[row,column] == snakeHead)
                {
                    bool player_crashed_with_borders   = map[row,column] == map[maxIndexRow-2, column];
                    bool player_crashed_with_snakebody = map[row+1,column] == snakeBody;
                    bool snake_is_going_to_the_right   = map[row+1,column] == backgroundMap &&  map[row-1,column] != snakeBody && map[row,column+1] != snakeBody;
                    bool snake_is_going_to_the_left    = map[row+1,column] == backgroundMap &&  map[row-1,column] != snakeBody && map[row,column-1] != snakeBody;
                    bool snake_is_going_down           = map[row+1,column] == backgroundMap && map[row-1, column] == snakeBody;

                    if (player_crashed_with_borders)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (player_crashed_with_snakebody)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (snake_is_going_to_the_right)
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row,column-snakeBodyHorizontalCounter] = backgroundMap;

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (snake_is_going_to_the_left)
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row,column+snakeBodyHorizontalCounter] = backgroundMap;

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (snake_is_going_down)
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row-snakeBodyVerticalCounter, column] = backgroundMap;                                               
                        return;
                    }        
                }
            }
        }
    }
        
    public void moveRight()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                if (map[row,column] == snakeHead)
                {
                    bool player_crashed_with_borders   = map[row,column] == map[row, maxIndexColumn-2];
                    bool player_crashed_with_snakebody = map[row,column+1] == snakeBody;
                    bool snake_is_going_up             = map[row,column+1] == backgroundMap && map[row,column-1] != snakeBody && map[row-1,column] != snakeBody;
                    bool snake_is_going_down           = map[row,column+1] == backgroundMap && map[row,column-1] != snakeBody && map[row+1,column] != snakeBody;
                    bool snake_is_going_to_the_right   = map[row,column+1] == backgroundMap && map[row,column-1] == snakeBody;

                    if (player_crashed_with_borders) 
                    {
                        playerLose = true;
                        return;
                    }
                    else if (player_crashed_with_snakebody)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (snake_is_going_up)
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row+snakeBodyVerticalCounter, column] = backgroundMap;

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (snake_is_going_down)
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = snakeBody;
                        
                        map[row-snakeBodyVerticalCounter, column] = backgroundMap;

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (snake_is_going_to_the_right)
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row, column-snakeBodyHorizontalCounter] = backgroundMap;
                        return;
                    }
                }
            }
        }
    }
    public void moveLeft()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                if (map[row,column] == snakeHead)
                {
                    bool player_crashed_with_borders   = map[row,column] == map[row,1];
                    bool player_crashed_with_snakebody = map[row,column-1] == snakeBody;
                    bool snake_is_going_up             = map[row,column-1] == backgroundMap && map[row,column+1] != snakeBody && map[row-1,column] != snakeBody;
                    bool snake_is_going_down           = map[row,column-1] == backgroundMap && map[row,column+1] != snakeBody && map[row+1,column] != snakeBody;
                    bool snake_is_going_to_the_left    = map[row,column-1] == backgroundMap && map[row,column+1] == snakeBody;

                    if (player_crashed_with_borders)
                    {
                        playerLose = true; 
                        return;
                    }
                    else if (player_crashed_with_snakebody)
                    {
                        playerLose = true;
                        return;
                    }
                    else if (snake_is_going_up)
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row+snakeBodyVerticalCounter, column] = backgroundMap;

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (snake_is_going_down)
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row-snakeBodyVerticalCounter, column] = backgroundMap;

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (snake_is_going_to_the_left)
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = snakeBody;
                        map[row, column+snakeBodyHorizontalCounter] = backgroundMap;
                        return;
                    }
                }
            }
        }
    }

    public void finishGame()
    {
        _userInterface.writeMessage($"-------------\nYOU CRASHED!\nScore: {score}\n-------------");
    }

}