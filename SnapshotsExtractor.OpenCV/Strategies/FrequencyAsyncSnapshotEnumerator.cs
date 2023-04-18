using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV.Strategies;

public class FrequencyAsyncSnapshotEnumerator : IAsyncSnapshotEnumerator
{
    private readonly VideoCapture _video;
    private double _frequencyInSeconds;
    private int _currentFrameIndex;
    private int _step;

    public FrequencyAsyncSnapshotEnumerator(VideoCapture capture, TimeSpan frequency)
    {
        _video = capture;
        Frequency = frequency;
        CalculateStepAndFrequency(frequency);
        _currentFrameIndex = 0;
    }

    public TimeSpan Frequency
    {
        get => TimeSpan.FromSeconds(_frequencyInSeconds);
        set => CalculateStepAndFrequency(value);
    }

    private void CalculateStepAndFrequency(TimeSpan frequency)
    {
        _frequencyInSeconds = frequency.TotalSeconds;
        _step = (int) Math.Round(_frequencyInSeconds * _video.Fps);
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
            if (!IsNextFrameExists)
            {
                return false;
            }

            Current?.Dispose();
            _video.PosFrames = _currentFrameIndex;
            _currentFrameIndex += _step;
            _video.Grab();
            using var image = _video.RetrieveMat();
            Current = new Frame(image);
            return true;
        });
    }

    public IFrame? Current { get; private set; }
}