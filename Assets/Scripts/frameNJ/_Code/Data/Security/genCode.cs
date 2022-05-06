using System.Text;

public static class genCode
{
    public static string Alphabet5()
    {
        StringBuilder sb = new StringBuilder(5);
        sb.Append(getRandCharExcepI(0, 23));
        sb.Append(getRandCharExcepI(0, chars.Length));
        sb.Append(getRandCharExcepI(0, chars.Length));
        sb.Append(getRandCharExcepI(0, chars.Length));
        sb.Append(getRandCharExcepI(24, chars.Length));
        return sb.ToString();
    }

    static string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";
    static char getRandCharExcepI(int minInclusive = 0, int maxExclusive = 23)
    {
        return chars[UnityEngine.Random.Range(minInclusive, maxExclusive)];
    }
}
