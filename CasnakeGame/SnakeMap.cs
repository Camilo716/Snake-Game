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


    public SnakeMap(int heightOfMap, int widthOfMap)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
        this._math = new SnakeMath();
        _gameComponents = new ConsoleGameComponents();
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
                    modifyActualCeil(row,column, _gameComponents.SnakeHead);
                    modifyCeilAhead("left", row, column, _gameComponents.SnakeBody);
                }
                else if (middle_of_map)
                {
                    modifyActualCeil(row,column, _gameComponents.Fruit);
                }
                else if (vertical_border_of_map || horizonal_border_of_map)
                {
                    modifyActualCeil(row,column, _gameComponents.BorderMap);
                }
                else
                {
                    modifyActualCeil(row,column, _gameComponents.Background);
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

            bool placeWithoutAnotherEntities = map[rowNewFruit, columnNewFruit] == _gameComponents.Background;

            if (placeWithoutAnotherEntities)
            {
                modifyActualCeil(rowNewFruit,columnNewFruit, _gameComponents.Fruit);
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

        if (ceilAhead == _gameComponents.BorderMap || ceilAhead == _gameComponents.SnakeBody)
        {
            return "collition";
        }
        else if (ceilAhead == _gameComponents.Fruit)
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
