namespace CoordinateGenerator.Motions;

public sealed class ErrorGenerator : IMotion
{
    public double MaxErrorPerMm { get; }
    private readonly Random _random;
    private readonly RandomBoolean _randomBoolean;
    private readonly IMotion _wrapped;

    public ErrorGenerator(double maxErrorPerMm, IMotion wrapped)
    {
        MaxErrorPerMm = maxErrorPerMm;
        _wrapped = wrapped;
        _random = new Random();
        _randomBoolean = new RandomBoolean();
    }

    public double GoToPosition(double position) =>
        CurrentActualPosition = _wrapped.GoToPosition(position) + GetNextError() * Math.Abs(position - CurrentTheoreticalPosition);

    public double CurrentActualPosition { get; private set; }
    public double CurrentTheoreticalPosition => _wrapped.CurrentTheoreticalPosition;
    public Direction Direction => _wrapped.Direction;
    public bool DirectionChanged => _wrapped.DirectionChanged;

    private double GetNextError() =>
        _random.NextDouble() * MaxErrorPerMm * _randomBoolean.GetPosOrNeg();
}