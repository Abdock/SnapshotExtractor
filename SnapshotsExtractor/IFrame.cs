namespace SnapshotsExtractor;

public interface IFrame : IDisposable
{
    ISnapshotDataChunkEnumerator GetEnumerator();
}