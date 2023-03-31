namespace SnapshotsExtractor;

public interface IVideoProcessor : IAsyncEnumerable<IFrame>
{
    IAsyncSnapshotEnumerator AsyncSnapshotEnumerator { get; set; }
}