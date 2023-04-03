using System.Buffers;
using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV;

public sealed class Frame : IFrame, IAsyncDisposable
{
    private readonly byte[] _data;
    private bool _disposed;
    private readonly ArrayPool<byte> _pool;

    private Frame()
    {
        _pool = ArrayPool<byte>.Create();
        _disposed = false;
        _data = Array.Empty<byte>();
    }
    
    public Frame(byte[] data) : this()
    {
        _data = data;
    }

    public Frame(Mat image) : this()
    {
        Cv2.ImEncode(".jpg", image, out _data);
    }

    public byte[] ToByte()
    {
        return _data;
    }

    public Task<byte[]> ToByteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_data);
    }

    private ValueTask Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _pool.Return(_data);
            }
            _disposed = true;
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        var task = Dispose(true);
        GC.SuppressFinalize(this);
        return task;
    }

    ~Frame()
    {
        Dispose(false);
    }
}