﻿using OpenCvSharp;
using SnapshotsExtractor.Exceptions;

namespace SnapshotsExtractor.OpenCV.Strategies;

public class FrequencySnapshotStrategy : ISnapshotStrategy
{
    private readonly VideoCapture _video;
    private double _frequencyInSeconds;
    private int _currentFrameIndex;
    private int _step;
    private const int DefaultFrequencyInSeconds = 5;

    public FrequencySnapshotStrategy(string videoPath)
    {
        _video = VideoCapture.FromFile(videoPath);
        var frequency = TimeSpan.FromSeconds(DefaultFrequencyInSeconds);
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

    public bool IsNextFrameExists => _currentFrameIndex + _step <= _video.FrameCount;

    public Task<IFrame> NextFrameAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            if (!IsNextFrameExists)
            {
                throw new FrameNotExistsException("Next frame is not exists because video stream is end");
            }

            _video.PosFrames = _currentFrameIndex;
            _currentFrameIndex += _step;
            _video.Grab();
            using var image = _video.RetrieveMat();
            IFrame frame = new Frame(image.ToBytes());
            return frame;
        }, cancellationToken);
    }
}