namespace CoordinateGenerator;

public sealed class CommandGenerator : IDisposable
{
    private readonly Random _random;
    private readonly double _max;

    public CommandGenerator(double max)
    {
        _random = new Random();
        _max = max;
    }

    public double GetNext() =>
        Math.Round(_random.NextDouble() * _max * PosOrNeg(), 5);

    private double PosOrNeg() =>
        Math.Round(_random.NextDouble()) == 0d ? -1d : 1d;
    public void Dispose()
    {
        
    }
}