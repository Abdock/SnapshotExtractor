using OpenCvSharp;
using SnapshotsExtractor;
using SnapshotsExtractor.OpenCV;
using SnapshotsExtractor.OpenCV.Strategies;

namespace ConsoleSample;

public class OpenCvSample
{
    public static async Task RunAsync()
    {
        const string path = @"<Path to your file>";
        using var video = VideoCapture.FromFile(path);
        IAsyncSnapshotEnumerator enumerator = new FrequencyAsyncSnapshotEnumerator(video, TimeSpan.FromSeconds(5));
        IVideoProcessor processor = new OpenCvVideoProcessor(enumerator);
        await foreach (var frame in processor)
        {
            Console.WriteLine((await frame.ToByteAsync()).Length);
        }
    }
}