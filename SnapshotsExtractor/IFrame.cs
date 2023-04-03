namespace SnapshotsExtractor;

public interface IFrame : IDisposable
{
    byte[] ToByte();

    Task<byte[]> ToByteAsync(CancellationToken cancellationToken = default);

    void SaveToFile(string file);
}