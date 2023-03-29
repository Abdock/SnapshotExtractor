namespace SnapshotsExtractor;

public interface IFrame
{
    byte[] ToByte();

    Task<byte[]> ToByteAsync(CancellationToken cancellationToken = default);
}