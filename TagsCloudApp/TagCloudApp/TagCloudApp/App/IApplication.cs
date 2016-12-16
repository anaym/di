namespace TagCloudApp.App
{
    public interface IApplication
    {
        void Run();

        void ChangeDocumentNewStatus(bool @new);
        void ChangeDocumentFileName(string name);
        void Notify(string message);
        string RequestSavePath(string fileName, string extension);
        string[] RequestOpenFiles(string extension);
        string RequestOpenFolders();
        void Request<T>(T settingsObject);
    }
}