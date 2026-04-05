using UnityEngine;

public static class Extensions
{
    public static int Mod(this int n, int m)
    {
        int r = n % m;
        return r < 0 ? r + m : r;
    }
}
