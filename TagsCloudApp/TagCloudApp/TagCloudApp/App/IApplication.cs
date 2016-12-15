namespace TagCloudApp.App
{
    public interface IApplication
    {
        void Run();

        string RequestSavePath(string fileName, string extension);
        string[] RequestOpenFiles(string extension);
        string RequestOpenFolders();
        void Request<T>(T settingsObject);
    }
}