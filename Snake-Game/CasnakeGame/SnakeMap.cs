namespace casnake.Game;
using casnake.SnakeUI;

public class SnakeMap
{
    public string[,] map;
    private int heightOfMap; // Rows
    private int widthOfMap;  // Columns
    private int maxIndexRow;
    private int maxIndexColumn;
    private IGameComponentsUI _gameComponents;


    public SnakeMap(int heightOfMap, int widthOfMap)
    {
        this.heightOfMap = heightOfMap;
        this.widthOfMap = widthOfMap;
        this.map = new string[heightOfMap,widthOfMap];
        this.maxIndexRow = this.map.GetLength(0);
        this.maxIndexColumn = this.map.GetLength(1);
        _gameComponents = new ConsoleGameComponents();
    }

    public void createInitialMap()
    {
        for (int row = 0; row < maxIndexRow; row++)
        {
            for (int column = 0; column < maxIndexColumn; column++)
            {
                bool place_to_create_snake = row == SnakeMath.round(maxIndexRow/2) && column == 2;
                bool middle_of_map = row == SnakeMath.round(maxIndexRow/2) && column == SnakeMath.round(maxIndexColumn/2);
                bool vertical_border_of_map = row == 0 || row == maxIndexRow-1;
                bool horizonal_border_of_map = column == 0 || column == maxIndexColumn-1;

                if (place_to_create_snake)
                {
                    map[row,column] = _gameComponents.SnakeHead;
                    map[row,column-1] = _gameComponents.SnakeBody;
                }
                else if (middle_of_map)
                {
                    map[row,column] = _gameComponents.Fruit;
                }
                else if (vertical_border_of_map || horizonal_border_of_map)
                {
                    map[row,column] = _gameComponents.BorderMap;
                }
                else
                {
                    map[row,column] = _gameComponents.Background;
                }
            }
        }
    }

    public void generateFruit()
    {
        while (true)
        {
            int rowNewFruit = SnakeMath.randomNumber(maxIndexRow);
            int columnNewFruit = SnakeMath.randomNumber(maxIndexColumn);

            bool placeWithoutAnotherComponents = map[rowNewFruit, columnNewFruit] == _gameComponents.Background;

            if (placeWithoutAnotherComponents)
            {
                map[rowNewFruit, columnNewFruit] = _gameComponents.Fruit;
                break;
            }
        }
    }
}
