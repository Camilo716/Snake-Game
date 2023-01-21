public class SnakeMath
{
    public int round(float val)
    {
        return Convert.ToInt32(Math.Floor(val));
    }

    public int randomNumberForRow(int maxIndexRow)
    {
        var randomNumber = new Random();

        return randomNumber.Next(1, maxIndexRow-1);
    }
    
    public int randomNumberForColumn(int maxIndexColumn)
    {
        var randomNumber = new Random();

        return  randomNumber.Next(1, maxIndexColumn-1);
    }
}


