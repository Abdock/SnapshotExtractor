namespace SnapshotsExtractor;

public interface IVideoProcessor
{
    Task<IFrame> TakeSnapshotAsync<TOriginalImage>(ISnapshotStrategy<TOriginalImage> snapshotStrategy);
    IAsyncEnumerable<IFrame> TakeSnapshotsAsync<TOriginalImage>(ISnapshotStrategy<TOriginalImage> snapshotStrategy);
}