global using NUnit.Framework;

SnakeGame game = new SnakeGame(20, 40);

game.playGame();

public class SnakeGame
{
    public int snakeBodyHorizontal = 1;
    public int snakeBodyVertical = 0;
    public string snakeHead = "@";
    public int score = 0;
    public int heightOfMap; // Rows
    public int widthOfMap;  // Columns
    public string[,] map;
    public string moveToDo;
    public int maxIndexRow;
    public int maxIndexColumn;
    

    public SnakeGame(int heightOfMap, int widthOfMap)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
    }

    public void playGame()
    {
        this.createInitialMap();
        this.writeMap();

        while (true)
        {
            this.moveToDo = Console.ReadLine();

            nextMove();

        }
    }

     public void createInitialMap()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                if (row == Math.Floor(Convert.ToDecimal(maxIndexRow/2)) && column == 2)
                {
                    this.map[row,column] = snakeHead;
                    this.map[row, column-1] = "0";
                }
                else if (row == Math.Floor(Convert.ToDecimal(maxIndexRow/2)) && column == Math.Floor(Convert.ToDecimal(maxIndexColumn/2)) )
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
            Console.Write("\n");
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
                Console.Write(map[row,column]);
            }
            Console.Write("\n");
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
                        finishGame(); 
                        return;
                    }
                    else if (map[row-1,column] == "0")
                    {
                        finishGame();
                        return;
                    }
                    else if (map[row-1,column] == "." &&  map[row+1,column] != "0" && map[row,column+1] != "0") // When snake is going right
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column-snakeBodyHorizontal] = ".";

                        this.snakeBodyVertical+=1;
                        this.snakeBodyHorizontal-=1;
                        return;
                    }
                    else if (map[row-1,column] == "." &&  map[row+1,column] != "0" && map[row,column-1] != "0") // When snake is going left
                    {
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column+snakeBodyHorizontal] = ".";

                        this.snakeBodyVertical+=1;
                        this.snakeBodyHorizontal-=1;
                        return;
                    }
                    else if (map[row-1,column] == "." && map[row+1, column] == "0") // When snake is going up
                    {
                       
                        map[row-1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row+snakeBodyVertical, column] = ".";
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
                        finishGame(); 
                        return;
                    }
                    else if (map[row+1,column] == "0")
                    {
                        finishGame();
                        return;
                    }
                    else if (map[row+1,column] == "." &&  map[row-1,column] != "0" && map[row,column+1] != "0") // When snake is going right
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column-snakeBodyHorizontal] = ".";

                        this.snakeBodyVertical+=1;
                        this.snakeBodyHorizontal-=1;
                        return;
                    }
                    else if (map[row+1,column] == "." &&  map[row-1,column] != "0" && map[row,column-1] != "0") // When snake is going left
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row,column+snakeBodyHorizontal] = ".";

                        this.snakeBodyVertical+=1;
                        this.snakeBodyHorizontal-=1;
                        return;
                    }
                    else if (map[row+1,column] == "." && map[row-1, column] == "0") // When snake is going down
                    {
                        map[row+1,column] = snakeHead;
                        map[row,column] = "0";
                        map[row-snakeBodyVertical, column] = ".";                                               
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
                        finishGame(); 
                        return;
                    }
                    else if (map[row,column+1] == "0")
                    {
                        finishGame(); 
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] != "0" && map[row-1,column] != "0") // When snake is going up
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        
                        map[row+snakeBodyVertical, column] = ".";

                        this.snakeBodyVertical-=1;
                        this.snakeBodyHorizontal+=1;
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] != "0" && map[row+1,column] != "0") // When snake is going down
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        
                        map[row-snakeBodyVertical, column] = ".";

                        this.snakeBodyVertical-=1;
                        this.snakeBodyHorizontal+=1;
                        return;
                    }
                    else if (map[row,column+1] == "." && map[row,column-1] == "0") // When snake is going right
                    {
                        map[row,column+1] = snakeHead;
                        map[row,column] = "0";
                        map[row, column-snakeBodyHorizontal] = ".";
                        return;
                    }

                    // if (map[row,column +1] == "#" && map[row,column +1] != "0" )
                    // {
                    //     map[row,column +1] = snakeHead;
                    //     map[row,column] = "0";
                    //     snakeBodyHorizontal +=1;
                    //     break;
                    // }
                    // if (map[row,column +1] == "#" && map[row,column +1] == "0" )
                    // {
                    //     map[row,column +1] = snakeHead;
                    //     map[row,column] = "0";
                    //     snakeBodyHorizontal +=1;
                    //     break;
                    // }
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
                        finishGame(); 
                        return;
                    }
                    else if (map[row,column-1] == "0")
                    {
                        finishGame(); 
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] != "0" && map[row-1,column] != "0") // When snake is going up
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row+snakeBodyVertical, column] = ".";

                        this.snakeBodyVertical-=1;
                        this.snakeBodyHorizontal+=1;
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] != "0" && map[row+1,column] != "0") // When snake is going down
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row-snakeBodyVertical, column] = ".";

                        this.snakeBodyVertical-=1;
                        this.snakeBodyHorizontal+=1;
                        return;
                    }
                    else if (map[row,column-1] == "." && map[row,column+1] == "0") // When snake is going left
                    {
                        map[row,column-1] = snakeHead;
                        map[row,column] = "0";
                        map[row, column+snakeBodyHorizontal] = ".";
                        return;
                    }

                    // if (map[row,column +1] == "#" && map[row,column +1] != "0" )
                    // {
                    //     map[row,column +1] = snakeHead;
                    //     map[row,column] = "0";
                    //     snakeBodyHorizontal +=1;
                    //     break;
                    // }
                    // if (map[row,column +1] == "#" && map[row,column +1] == "0" )
                    // {
                    //     map[row,column +1] = snakeHead;
                    //     map[row,column] = "0";
                    //     snakeBodyHorizontal +=1;
                    //     break;
                    // }
                }
            }
        }
    }

    public void finishGame()
    {
        Console.WriteLine($"-------------\nYOU CRASHED!\nScore: {score} \n-------------");
    }

}