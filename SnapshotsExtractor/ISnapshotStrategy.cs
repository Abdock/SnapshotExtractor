namespace SnapshotsExtractor;

public interface ISnapshotStrategy
{
    bool IsNextFrameExists { get; }
    Task<IFrame> NextFrameAsync(CancellationToken cancellationToken = default);
}