namespace SnapshotsExtractor;

public interface IFrame : IDisposable
{
    ISnapshotDataChunkEnumerator GetEnumerator();

    void SaveInFile(string file);
}