namespace casnake.Game;
public static class SnakeMath
{
    static public int round(float val)
    {
        return Convert.ToInt32(Math.Floor(val));
    }

    static public int randomNumber(int maxNumber)
    {
        var randomNumber = new Random();

        return randomNumber.Next(1, maxNumber-1);
    }
}


