using System.Buffers;
using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV.Strategies;

public class CascadeContainsAsyncSnapshotEnumerator : IAsyncSnapshotEnumerator
{
    private readonly VideoCapture _video;
    private readonly CascadeClassifier _classifier;
    private readonly int _step;
    private int _currentFrameIndex;
    private readonly ArrayPool<byte> _pool;

    public CascadeContainsAsyncSnapshotEnumerator(VideoCapture video, CascadeClassifier classifier)
    {
        _video = video;
        _classifier = classifier;
        _step = (int) Math.Round(_video.Fps);
        _pool = ArrayPool<byte>.Create();
    }

    private bool IsNextFrameExists => _currentFrameIndex + _step <= _video.FrameCount;

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        return await Task.Run(() =>
        {
            Rect[] rects;
            IFrame? frame = null;
            do
            {
                _video.PosFrames = _currentFrameIndex;
                _currentFrameIndex += _step;
                _video.Grab();
                using var image = _video.RetrieveMat();
                rects = _classifier.DetectMultiScale(image);
                if (rects.Any())
                {
                    frame = new Frame(image);
                }
            } while (!rects.Any() && IsNextFrameExists);

            if (frame is null)
            {
                return false;
            }

            Current = frame;
            return true;
        });
    }

    public IFrame Current { get; private set; } = null!;
}