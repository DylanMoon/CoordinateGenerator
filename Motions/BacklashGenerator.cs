namespace CoordinateGenerator.Motions;

public sealed class BacklashGenerator : IMotion
{
    private readonly IMotion _wrapped;
    private readonly Random _random;
    public double BacklashAmount { get; }

    public BacklashGenerator(double backlashAmount, IMotion wrapped)
    {
        BacklashAmount = backlashAmount;
        _random = new Random();
        _wrapped = wrapped;
    }

    public double GoToPosition(double position) =>
        CurrentActualPosition = _wrapped.GoToPosition(position) + GetBacklash(DirectionChanged);

    public double GetBacklash(bool dirChange) =>
        !dirChange ? 0d
        : Direction == Direction.Positive ? - BacklashAmount * _random.NextDouble()
        : BacklashAmount * _random.NextDouble();

    public double CurrentActualPosition { get; private set; }
    public double CurrentTheoreticalPosition => _wrapped.CurrentTheoreticalPosition;
    public Direction Direction => _wrapped.Direction;
    public bool DirectionChanged => _wrapped.DirectionChanged;
}