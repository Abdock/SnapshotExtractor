using System.Runtime.Serialization;

namespace SnapshotsExtractor.Exceptions;

public class FrameNotExistsException : Exception
{
    public FrameNotExistsException()
    {
    }

    protected FrameNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FrameNotExistsException(string? message) : base(message)
    {
    }

    public FrameNotExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}