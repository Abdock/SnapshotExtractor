namespace SnapshotsExtractor;

public interface ISnapshotDataChunkEnumerator
{
    bool MoveNext();

    byte[] Current { get; }
}