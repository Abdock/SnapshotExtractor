using System.Runtime.CompilerServices;

namespace SnapshotsExtractor.OpenCV;

public class OpenCvVideoProcessor : IVideoProcessor
{
    public ISnapshotStrategy SnapshotStrategy { get; set; }

    public OpenCvVideoProcessor(ISnapshotStrategy snapshotStrategy)
    {
        SnapshotStrategy = snapshotStrategy;
    }

    public async Task<IFrame> TakeSnapshotAsync(CancellationToken cancellationToken = default)
    {
        return await SnapshotStrategy.NextFrameAsync(cancellationToken);
    }

    public async IAsyncEnumerable<IFrame> TakeSnapshotsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (SnapshotStrategy.IsNextFrameExists)
        {
            yield return await SnapshotStrategy.NextFrameAsync(cancellationToken);
        }
    }
}