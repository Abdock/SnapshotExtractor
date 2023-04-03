using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV;

public sealed class Frame : IFrame
{
    private readonly Mat _image;

    public Frame(Mat image)
    {
        _image = image.Clone();
    }

    public ISnapshotDataChunkEnumerator GetEnumerator()
    {
        return new FrameDataEnumerator(_image);
    }

    public void Dispose()
    {
        _image.Dispose();
    }

    public struct FrameDataEnumerator : ISnapshotDataChunkEnumerator
    {
        private readonly Mat _image;
        private int _currentIndex;
        private const int ChunkLength = 16 * 1024;
        private readonly long _endIndex;
        
        public FrameDataEnumerator(Mat mat)
        {
            Current = new byte[ChunkLength];
            _currentIndex = 0;
            _image = mat;
            _endIndex = mat.Total() * mat.Channels();
        }
        
        public bool MoveNext()
        {
            if (_currentIndex < _endIndex)
            {
                var length = (int) Math.Min(_endIndex - _currentIndex, ChunkLength);
                unsafe
                {
                    var ptr = _image.DataPointer;
                    for (var i = 0; i < length; ++i)
                    {
                        Current[i] = *(ptr + i);
                    }
                }
                _currentIndex += length;
                return true;
            }

            return false;
        }

        public byte[] Current { get; }

        public void Dispose()
        {
            if (!_image.IsDisposed)
            {
                _image.Dispose();
            }
        }
    }
}