namespace SnapshotsExtractor.OpenCV;

public class Frame : IFrame
{
    private readonly byte[] _data;

    public Frame(byte[] data)
    {
        _data = data;
    }

    public byte[] ToByte()
    {
        return _data;
    }

    public Task<byte[]> ToByteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_data);
    }
}