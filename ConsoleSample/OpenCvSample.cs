using SnapshotsExtractor;
using SnapshotsExtractor.OpenCV;
using SnapshotsExtractor.OpenCV.Strategies;

namespace ConsoleSample;

public class OpenCvSample
{
    public static async Task RunAsync()
    {
        const string path = "<Your video path>";
        ISnapshotStrategy strategy = new FrequencySnapshotStrategy(path);
        IVideoProcessor processor = new OpenCvVideoProcessor(strategy);
        var frame = await processor.TakeSnapshotAsync();
        var data = await frame.ToByteAsync();
        Console.WriteLine(string.Join(", ", data));
    }
}