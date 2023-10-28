namespace CoordinateGenerator.Motions;

public sealed class MotionGenerator : IMotion
{
    public MotionGenerator(double pitch, double stepsPerRevolution, double microSteps = 1d) =>
        MmTraveledPerStep = 1d / (stepsPerRevolution * microSteps / pitch);

    public double GoToPosition(double value) =>
        CurrentActualPosition = GetNextPosition((int) (Math.Abs(value - CurrentActualPosition) / MmTraveledPerStep + .5d),
            Direction = value > CurrentActualPosition ? Direction.Positive : Direction.Negative);

    private double GetNextPosition(int numberOfSteps, Direction positiveDir) =>
        positiveDir == Direction.Positive
            ? CurrentActualPosition + numberOfSteps * MmTraveledPerStep
            : CurrentActualPosition - numberOfSteps * MmTraveledPerStep;

    public double CurrentActualPosition { get; private set; }
    public double CurrentTheoreticalPosition => CurrentActualPosition;
    public double MmTraveledPerStep { get; }

    private Direction _direction = Direction.Positive;
    public Direction Direction
    {
        get => _direction;
        private set
        {
            DirectionChanged = _direction != value;
            _direction = value;
        }
    }

    public bool DirectionChanged { get; private set; }
}