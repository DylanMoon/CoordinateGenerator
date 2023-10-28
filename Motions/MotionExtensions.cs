namespace CoordinateGenerator.Motions;

public static class MotionExtensions
{
    public static IMotion WithBacklash(this IMotion motion, double backlash) =>
        new BacklashGenerator(backlash, motion);

    public static IMotion WithErrorPerMm(this IMotion motion, double maxError) =>
        new ErrorGenerator(maxError, motion);
}