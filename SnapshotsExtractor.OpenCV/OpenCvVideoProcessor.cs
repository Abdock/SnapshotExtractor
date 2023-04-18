namespace SnapshotsExtractor.OpenCV;

public class OpenCvVideoProcessor : IVideoProcessor
{
    public IAsyncSnapshotEnumerator AsyncSnapshotEnumerator { get; set; }

    public OpenCvVideoProcessor(IAsyncSnapshotEnumerator asyncSnapshotEnumerator)
    {
        AsyncSnapshotEnumerator = asyncSnapshotEnumerator;
    }

    public IAsyncEnumerator<IFrame> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return AsyncSnapshotEnumerator;
    }
}