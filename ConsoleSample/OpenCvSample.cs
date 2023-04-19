using System.Diagnostics;
using OpenCvSharp;
using SnapshotsExtractor;
using SnapshotsExtractor.OpenCV;
using SnapshotsExtractor.OpenCV.Strategies;

namespace ConsoleSample;

public static class OpenCvSample
{
    public static async Task RunAsync()
    {
        const string videoPath = @"https://rr2---sn-5hne6ns6.googlevideo.com/videoplayback?expire=1681934248&ei=SPM_ZKLVDajRxgKm94igBw&ip=37.120.218.92&id=o-AMbfxx-RxNIj6DzfhLcmiO0VDFp9HvnUH1fISDdFAF4E&itag=22&source=youtube&requiressl=yes&mh=II&mm=31%2C26&mn=sn-5hne6ns6%2Csn-4g5ednly&ms=au%2Conr&mv=m&mvi=2&pl=24&initcwndbps=768750&spc=99c5CczNxrzIXYiTQ5qmWWIS-wQ8A1UVfNonUwT5EA&vprv=1&mime=video%2Fmp4&ns=eGj3ntyhIDOpyUyiTqIe3qsM&cnr=14&ratebypass=yes&dur=10176.702&lmt=1678552862943321&mt=1681911911&fvip=3&fexp=24007246&c=WEB&txp=5318224&n=OhHL7-PEG839DQ&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cspc%2Cvprv%2Cmime%2Cns%2Ccnr%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRQIhAJJQhEw8sMeyMBQv6dAa6fjuW9taCrRprXu5zp5F_oIOAiBSNvtoS8MXqyYZIFAqmJG8kbPwBet87L16G1f9pMcKrQ%3D%3D&lsparams=mh%2Cmm%2Cmn%2Cms%2Cmv%2Cmvi%2Cpl%2Cinitcwndbps&lsig=AG3C_xAwRAIgBCx-me1vrbTzfSjKxdJYT8ea14eT_SzF6s7yfvCHXKQCIDg_9yhPNLTUPx_A5GOHAd6aoeLMBklMXXbIbRv9B5GJ&title=C%2B%2B%204.%20%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D1%8F%20std%3A%3Amove%2C%20%20rvalue%20and%20lvalue%20%2B%20%D0%A1%D0%B5%D0%BC%D0%B8%D0%BD%D0%B0%D1%80";
        const string folder = @"C:\FFmpeg";
        using var video = VideoCapture.FromFile(videoPath);
        IAsyncSnapshotEnumerator enumerator = new FrequencyAsyncSnapshotEnumerator(video, TimeSpan.FromMinutes(5));
        IVideoProcessor processor = new OpenCvVideoProcessor(enumerator);
        var timer = new Stopwatch();
        timer.Start();
        var count = 0;
        await foreach (var frame in processor)
        {
            frame.SaveInFile($@"{folder}\{count++}.jpg");
        }
        timer.Stop();
        Console.WriteLine(timer.Elapsed);
        await enumerator.DisposeAsync();
    }
}