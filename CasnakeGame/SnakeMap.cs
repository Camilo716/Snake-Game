namespace casnake.Game;

public class SnakeMap
{
    public string[,] map;
    private int heightOfMap; // Rows
    private int widthOfMap;  // Columns
    private int maxIndexRow;
    private int maxIndexColumn;
    public Entities _entities;
    private SnakeMath _math;

    public SnakeMap(int heightOfMap, int widthOfMap)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
        this._entities = new Entities();
        this._math = new SnakeMath();
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
                    this.map[row,column] = _entities.snakeHead;
                    this.map[row, column-1] = _entities.snakeBody;
                }
                else if (middle_of_map)
                {
                    this.map[row,column] = _entities.fruit;
                }
                else if (vertical_border_of_map)
                {
                    this.map[row,column] = _entities.borderOfMap;
                }
                else if (horizonal_border_of_map)
                {
                    this.map[row,column] = _entities.borderOfMap;
                }
                else{
                    this.map[row,column] = _entities.backgroundMap;
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

            bool placeWithoutAnotherEntities = map[rowNewFruit, columnNewFruit] == _entities.backgroundMap;

            if (placeWithoutAnotherEntities)
            {
                map[rowNewFruit, columnNewFruit] = _entities.fruit;
                break;
            }
        }
    }
}
