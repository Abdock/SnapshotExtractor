using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV.Encoders.JPEG;

internal class JpegPixel
{
    public byte Y { get; }
    public byte Cb { get; }
    public byte Cr { get; }

    public JpegPixel(Vec3b pixel) : this(pixel.Item0, pixel.Item1, pixel.Item2)
    {
    }

    public JpegPixel(byte red, byte green, byte blue)
    {
        Y = (byte) (0.299m * red + 0.587m * green + 0.114m * blue);
        Cb = (byte) (-0.1687m * red - 0.3313m * green + 0.5m * blue + 128);
        Cr = (byte) (0.5m * red - 0.4187m * green + 0.0813m * blue + 128);
    }
}