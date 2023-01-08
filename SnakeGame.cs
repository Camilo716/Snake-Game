public class SnakeGame
{
    public int snakeBodyHorizontalCounter = 1;
    public int snakeBodyVerticalCounter = 0;
    public string snakeHead = "@";
    public int score = 0;
    public bool playerLose = false;
    public int heightOfMap; // Rows
    public int widthOfMap;  // Columns
    public string[,] map;
    public string moveToDo;
    public int maxIndexRow;
    public int maxIndexColumn;
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
                if (row == _math.Round(maxIndexRow/2) && column == 2)
                {
                    this.map[row,column] = snakeHead;
                    this.map[row, column-1] = "0";
                }
                else if (row == _math.Round(maxIndexRow/2) && column == _math.Round(maxIndexColumn/2) )
                {
                    this.map[row,column] = "#";
                }
                else if (row == 0 || row == maxIndexRow-1)
                {
                    this.map[row,column] = "_";
                }
                else if (column == 0 || column == maxIndexColumn-1)
                {
                    this.map[row,column] = "|";
                }
                else{
                    this.map[row,column] = ".";
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
                    if (map[row,column] == map[1, column])
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row-1,column] == "0")
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row-1,column] == "." &&  map[row+1,column] != "0" && map[row,column+1] != "0") // When snake is going right
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column-snakeBodyHorizontalCounter] = ".";

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (map[row-1,column] == "." &&  map[row+1,column] != "0" && map[row,column-1] != "0") // When snake is going left
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column+snakeBodyHorizontalCounter] = ".";

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (map[row-1,column] == "." && map[row+1, column] == "0") // When snake is going up
                    {
                       
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row+snakeBodyVerticalCounter, column] = ".";
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
                    if (map[row,column] == map[maxIndexRow-2, column])
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row+1,column] == "0")
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row+1,column] == "." &&  map[row-1,column] != "0" && map[row,column+1] != "0") // When snake is going right
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column-snakeBodyHorizontalCounter] = ".";

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (map[row+1,column] == "." &&  map[row-1,column] != "0" && map[row,column-1] != "0") // When snake is going left
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column+snakeBodyHorizontalCounter] = ".";

                        this.snakeBodyVerticalCounter+=1;
                        this.snakeBodyHorizontalCounter-=1;
                        return;
                    }
                    else if (map[row+1,column] == "." && map[row-1, column] == "0") // When snake is going down
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row-snakeBodyVerticalCounter, column] = ".";                                               
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
                    if (map[row,column] == map[row, maxIndexColumn-2]) 
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row,column+1] == "0")
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] != "0" && map[row-1,column] != "0") // When snake is going up
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        
                        map[row+snakeBodyVerticalCounter, column] = ".";

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] != "0" && map[row+1,column] != "0") // When snake is going down
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        
                        map[row-snakeBodyVerticalCounter, column] = ".";

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] == "0") // When snake is going right
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        map[row, column-snakeBodyHorizontalCounter] = ".";
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
                    if (map[row,column] == map[row,1])
                    {
                        playerLose = true; 
                        return;
                    }
                    else if (map[row,column-1] == "0")
                    {
                        playerLose = true;
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] != "0" && map[row-1,column] != "0") // When snake is going up
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row+snakeBodyVerticalCounter, column] = ".";

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] != "0" && map[row+1,column] != "0") // When snake is going down
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row-snakeBodyVerticalCounter, column] = ".";

                        this.snakeBodyVerticalCounter-=1;
                        this.snakeBodyHorizontalCounter+=1;
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] == "0") // When snake is going left
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row, column+snakeBodyHorizontalCounter] = ".";
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