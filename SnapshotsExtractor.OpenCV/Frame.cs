using System.Buffers;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV;

public sealed class Frame : IFrame
{
    private readonly byte[] _data;
    private bool _disposed;
    private readonly ArrayPool<byte> _pool;

    private Frame()
    {
        _pool = ArrayPool<byte>.Create();
        _disposed = false;
    }

    public Frame(Mat image) : this()
    {
        var size = (int)image.Total() * image.Channels();
        _data = _pool.Rent(size);
        Marshal.Copy(image.Data, _data, 0, _data.Length);
    }

    public byte[] ToByte()
    {
        return _data;
    }

    public Task<byte[]> ToByteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_data);
    }

    public void SaveToFile(string file)
    {
        using var mat = new Mat(new[] {720, 1280}, MatType.CV_8UC3);
        Marshal.Copy(_data, 0, mat.Data, _data.Length);
        mat.SaveImage(file);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _pool.Return(_data);
        }

        _disposed = true;
    }
}