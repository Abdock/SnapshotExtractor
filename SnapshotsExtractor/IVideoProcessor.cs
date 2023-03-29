namespace SnapshotsExtractor;

public interface IVideoProcessor
{
    ISnapshotStrategy SnapshotStrategy { get; set; }

    Task<IFrame> TakeSnapshotAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<IFrame> TakeSnapshotsAsync(CancellationToken cancellationToken = default);
}