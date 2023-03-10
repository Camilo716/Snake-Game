namespace casnake.Game;
using casnake.SnakeUI;

public class SnakeMap
{
    public string[,] map;
    private SnakeMath _math;
    private int heightOfMap; // Rows
    private int widthOfMap;  // Columns
    private int maxIndexRow;
    private int maxIndexColumn;
    private string? ceilAhead;

    private IGameComponentsUI _gameComponents;
    private string snakeBody;
    private string snakeHead;
    private string backgroundMap;
    private string fruit;
    private string borderMap;

    public SnakeMap(int heightOfMap, int widthOfMap)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
        this._math = new SnakeMath();

        _gameComponents = new ConsoleGameComponents();
        this.snakeHead = _gameComponents.getGameComponent("SnakeHead");
        this.snakeBody = _gameComponents.getGameComponent("SnakeBody");
        this.fruit = _gameComponents.getGameComponent("Fruit");
        this.borderMap = _gameComponents.getGameComponent("BorderMap");
        this.backgroundMap = _gameComponents.getGameComponent("Background");
    }

    public void createInitialMap()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                bool place_to_create_snake = row == _math.round(maxIndexRow/2) && column == 2;
                bool middle_of_map = row == _math.round(maxIndexRow/2) && column == _math.round(maxIndexColumn/2);
                bool vertical_border_of_map = row == 0 || row == maxIndexRow-1;
                bool horizonal_border_of_map = column == 0 || column == maxIndexColumn-1;

                if (place_to_create_snake)
                {
                    modifyActualCeil(row,column, snakeHead);
                    modifyCeilAhead("left", row, column, snakeBody);
                }
                else if (middle_of_map)
                {
                    modifyActualCeil(row,column, fruit);
                }
                else if (vertical_border_of_map || horizonal_border_of_map)
                {
                    modifyActualCeil(row,column, borderMap);
                }
                else
                {
                    modifyActualCeil(row,column, backgroundMap);
                }
            }
        }
    }

    public void generateFruit()
    {
        while (true)
        {
            int rowNewFruit = _math.randomNumberForRow(maxIndexRow);
            int columnNewFruit = _math.randomNumberForColumn(maxIndexColumn);

            bool placeWithoutAnotherEntities = map[rowNewFruit, columnNewFruit] == backgroundMap;

            if (placeWithoutAnotherEntities)
            {
                modifyActualCeil(rowNewFruit,columnNewFruit, fruit);
                break;
            }
        }
    }

    public void modifyActualCeil(int row, int column, string gameComponentToPut)
    {
        map[row,column] = gameComponentToPut;
    }

    public void modifyCeilAhead(string direction, int row, int column, string gameComponentToPut)
    {
        switch (direction)
        {
            case "up":
                map[row-1,column] = gameComponentToPut;
                break;
            case "down":
                map[row+1,column] = gameComponentToPut;
                break;
            case "right":
                map[row,column+1] = gameComponentToPut;
                break;
            case "left":
                map[row,column-1] = gameComponentToPut;
                break;
            default:
                break;
       }      
    }

    public string whatIsAhead(string direction, int row,int column)
    {
        ceilAhead = getCeilAhead(direction,row,column);

        if (ceilAhead == borderMap || ceilAhead == snakeBody)
        {
            return "collition";
        }
        else if (ceilAhead == fruit)
        {
            return "fruit";       
        }
        return "";
    }

    private string getCeilAhead(string direction, int row, int column)
    {
        switch (direction)
        {
            case "up":
                return map[row-1,column];
            case "down":
                return map[row+1,column];
            case "right":
                return map[row,column+1];
            case "left":
                return map[row,column-1];     
            default:
                return "";
       }   
    }
}
