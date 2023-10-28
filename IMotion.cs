namespace CoordinateGenerator;

public interface IMotion
{
    double GoToPosition(double position);
    double CurrentActualPosition { get; }
    double CurrentTheoreticalPosition { get; }
    Direction Direction { get; }
    bool DirectionChanged { get; }
}