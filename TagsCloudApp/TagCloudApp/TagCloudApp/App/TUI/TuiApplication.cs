namespace TagCloudApp.App.TUI
{
    public class TuiApplication : IApplication
    {
        public void Run()
        {
            TuiEngine.Instance.Run(new TuiForm());
        }

        public void ChangeDocumentNewStatus(bool @new)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDocumentFileName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Notify(string message)
        {
            throw new System.NotImplementedException();
        }

        public string RequestSavePath(string fileName, string extension)
        {
            throw new System.NotImplementedException();
        }

        public string[] RequestOpenFiles(string extension)
        {
            throw new System.NotImplementedException();
        }

        public string RequestOpenFolders()
        {
            throw new System.NotImplementedException();
        }

        public void Request<T>(T settingsObject)
        {
            throw new System.NotImplementedException();
        }
    }
}