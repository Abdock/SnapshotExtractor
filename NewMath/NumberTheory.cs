namespace NewMath;

public static class NumberTheory
{
    public static int Gcd(int a, int b)
    {
        while (b > 0)
        {
            a %= b;
            (a, b) = (b, a);
        }

        return a;
    }
}