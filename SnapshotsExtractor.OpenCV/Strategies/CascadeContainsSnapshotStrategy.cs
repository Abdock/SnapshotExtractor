using OpenCvSharp;
using SnapshotsExtractor.Exceptions;

namespace SnapshotsExtractor.OpenCV.Strategies;

public class CascadeContainsSnapshotStrategy : ISnapshotStrategy
{
    private readonly VideoCapture _video;
    private readonly CascadeClassifier _classifier;
    private readonly int _step;
    private int _currentFrameIndex;

    public CascadeContainsSnapshotStrategy(string path, string trainedModelPath)
    {
        _video = VideoCapture.FromFile(path);
        _classifier = new CascadeClassifier(trainedModelPath);
        _step = (int) Math.Round(_video.Fps);
    }

    public bool IsNextFrameExists => _currentFrameIndex + _step <= _video.FrameCount;

    public Task<IFrame> NextFrameAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
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
                    frame = new Frame(image.ToBytes());
                }
            } while (!rects.Any() && IsNextFrameExists);

            if (frame is null)
            {
                throw new FrameNotExistsException("Next frame is not exists");
            }

            return frame;
        }, cancellationToken);
    }
}