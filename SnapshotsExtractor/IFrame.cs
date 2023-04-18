namespace SnapshotsExtractor;

public interface IFrame : IDisposable
{
    void SaveInFile(string path);
}