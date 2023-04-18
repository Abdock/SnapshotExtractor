using OpenCvSharp;

namespace SnapshotsExtractor.OpenCV;

public sealed class Frame : IFrame
{
    private readonly Mat _image;

    public Frame(Mat image)
    {
        _image = image.Clone();
    }

    public void SaveInFile(string path)
    {
        _image.SaveImage(path);
    }

    public void Dispose()
    {
        _image.Dispose();
    }
}