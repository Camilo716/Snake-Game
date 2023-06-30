namespace casnake.Game;
public static class SnakeMath
{
    static public int round(float val)
    {
        return Convert.ToInt32(Math.Floor(val));
    }

    static public int randomNumberForRow(int maxIndexRow)
    {
        var randomNumber = new Random();

        return randomNumber.Next(1, maxIndexRow-1);
    }
    
    static public int randomNumberForColumn(int maxIndexColumn)
    {
        var randomNumber = new Random();

        return  randomNumber.Next(1, maxIndexColumn-1);
    }
}


