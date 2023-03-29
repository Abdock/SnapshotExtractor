namespace SnapshotsExtractor;

public interface ISnapshotStrategy<TOriginalImage>
{
    Task<TOriginalImage> NextImageAsync();
}