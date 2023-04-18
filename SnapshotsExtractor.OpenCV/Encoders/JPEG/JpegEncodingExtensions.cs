using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV.Encoders.JPEG;

internal static class JpegEncodingExtensions
{
    public static JpegPixel ToYCrCb(this Vec3b openCvPixel)
    {
        return new JpegPixel(openCvPixel);
    }
}