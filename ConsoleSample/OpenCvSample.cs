using System.Diagnostics;
using OpenCvSharp;
using SnapshotsExtractor;
using SnapshotsExtractor.OpenCV;
using SnapshotsExtractor.OpenCV.Strategies;

namespace ConsoleSample;

public class OpenCvSample
{
    public static async Task RunAsync()
    {
        const string path = @"<Path to file or url>";
        using var video = VideoCapture.FromFile(path);
        IAsyncSnapshotEnumerator enumerator = new FrequencyAsyncSnapshotEnumerator(video, TimeSpan.FromSeconds(5));
        IVideoProcessor processor = new OpenCvVideoProcessor(enumerator);
        // to bytes network 00:00:30.4436376, local: 00:00:03.4846308, 00:00:03.3177733
        // optimal network: 00:00:28.0389142, local: 00:00:01.9220643, 00:00:01.9387679
        var timer = new Stopwatch();
        timer.Start();
        await foreach (var frame in processor)
        {
            var length = 0;
            foreach (var chunk in frame)
            {
                length += chunk.Length;
            }

            Console.WriteLine(length);
        }
        timer.Stop();
        Console.WriteLine(timer.Elapsed);
        await enumerator.DisposeAsync();
    }
}