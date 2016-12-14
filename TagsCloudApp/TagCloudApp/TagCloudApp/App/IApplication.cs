namespace TagCloudApp.App
{
    public interface IApplication
    {
        string RequestSavePath(string fileName, string extension);
        string[] RequestOpenFiles(string extension);
        string RequestOpenFolders();
        void Run();
    }
}