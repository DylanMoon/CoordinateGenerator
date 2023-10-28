using System.Diagnostics;
using System.Text;
using CoordinateGenerator;
using CoordinateGenerator.Motions;

var sw = new Stopwatch();
Console.WriteLine("Generating test data");
const string basePath = "";
const string filename = "leadscrew_02_200_0_1524_001";
/*********************************************************
 ================== FOUND PARAMETERS =====================
 pitch of ball screw = 5mm
 backlash of ball screw = .05mm (.002in)
 pitch of lead screw = 2mm
 backlash of lead screw = .1524mm (.002in - .010in)
**********************************************************/
var bs = new MotionGenerator(2d, 200d).WithBacklash(.1524d).WithErrorPerMm(.001d);
var cg = new CommandGenerator(10d);

var path = GetNextPath();
sw.Reset();
sw.Start();
var stream = File.OpenWrite(path);
stream.Write("commanded position, theoretical position, actual position, error\n"u8);
foreach (var pos in Enumerable.Range(0, 15000).Select(_=>cg.GetNext()))
{
    var commanded = pos;
    var actual = bs.GoToPosition(commanded);
    var theoretical = bs.CurrentTheoreticalPosition;
    stream.Write(Encoding.UTF8.GetBytes($"{commanded},{Math.Round(theoretical, 5)},{Math.Round(actual,5)},{Math.Round(Math.Abs(actual) - Math.Abs(commanded), 5)}\n"));
    // Console.WriteLine($"commanded: {commanded}, theoretical: {theoretical}, actual: {actual}, error: {Math.Round(Math.Abs(actual) - Math.Abs(commanded), 5)}\n");
}

stream.Flush();
stream.Dispose();
sw.Stop();
Console.WriteLine($"Completed in {sw.ElapsedMilliseconds}ms");
return;

string GetNextPath() => GetNextPathHelper(string.Empty, 1);

string GetNextPathHelper(string modifier, int count) => File.Exists(Path.Combine(basePath, $"{filename}{modifier}.csv"))
    ? GetNextPathHelper($"({count})", count + 1)
    : Path.Combine(basePath, $"{filename}{modifier}.csv");