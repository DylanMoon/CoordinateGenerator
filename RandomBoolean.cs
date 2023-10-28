namespace CoordinateGenerator;

public sealed class RandomBoolean
{
    private readonly Random _random;

    public RandomBoolean()
    {
        _random = new Random();
    }

    public bool GetNextBoolean() =>
        _random.Next(2) == 1;

    public double GetPosOrNeg() => GetNextBoolean() ? 1d : -1d;
}