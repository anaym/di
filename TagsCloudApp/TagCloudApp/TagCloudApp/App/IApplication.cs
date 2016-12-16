namespace TagCloudApp.App
{
    public interface IApplication
    {
        void Run();

        bool HasUnapplayedChanges { set; }
        string DocumentFileName { set; }
        void Notify(string message);
        string RequestSavePath(string fileName, string extension);
        string[] RequestOpenFiles(string extension);
        string RequestOpenFolders();
        void Request<T>(T settingsObject);
    }
}