using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TagCloud.GUI.Extensions;
using TagCloud.Settings;
using Utility.RailwayExceptions;
using Utility.RailwayExceptions.Extensions;

namespace TagCloud.GUI
{
    public class MainForm : Form
    {
        private readonly TagCloudCreator creator;
        private readonly LoaderSettings loaderSettings;
        private readonly string label;
        private string documentFileName;
        private bool hasUnapplayedChanges;
        private readonly MenuStrip mainMenu;
        private readonly PictureBox pictureBox;

        public MainForm(ISettings[] settings, TagCloudCreator creator, LoaderSettings loaderSettings)
        {
            this.creator = creator;
            this.loaderSettings = loaderSettings;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(512, 512);

            mainMenu = new MenuStrip {Dock = DockStyle.Bottom};
            mainMenu.ConnectCategory("File")
                .Connect("Open", LoadTags)
                .Connect("Save", Save)
                .ConnectSeparator()
                .Connect("Exit", Close);
            mainMenu.Items.AddRange(settings.ToMenuItems("Settings"));
            mainMenu.ConnectCategory("Render", Render);
            Controls.Add(mainMenu);

            Controls.Add(pictureBox = new PictureBox { Dock = DockStyle.Fill });

            label = "Tag Cloud Layouter";
            DocumentFileName = "";
            HasUnapplayedChanges = false;
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        public void LoadTags()
        {
            var dialog = new OpenFileDialog {CheckPathExists = true};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                loaderSettings.FileInfo = new FileInfo(dialog.FileName);
                creator.Load()
                    .OnAllErrorNotify()
                    .Execute(() => HasUnapplayedChanges = true)
                    .Execute(() => DocumentFileName = loaderSettings.FileInfo.Name);
            }
        }

        public void Save()
        {
            var dialog = new SaveFileDialog {CheckPathExists = true, DefaultExt = ".png", Filter = "Image |.png"};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Result
                    .Of(() => pictureBox.Image.Save(dialog.FileName, ImageFormat.Png))
                    .RefineError("Save error")
                    .OnErrorNotify();
            }
        }

        public void Render()
        {
            pictureBox.Image = null;
            creator
                .Render()
                .Execute(b => pictureBox.Image = b)
                .Execute(b => pictureBox.Size = b.Size)
                .RefineError("Render error")
                .OnErrorNotify()
                .Execute(() => pictureBox.Refresh())  
                .Execute(() => HasUnapplayedChanges = false);   
        }

        public bool HasUnapplayedChanges { set { hasUnapplayedChanges = value; UpdateTitle(); } }
        public string DocumentFileName { set { documentFileName = value; UpdateTitle(); } }
        private void UpdateTitle()
        {
            if (string.IsNullOrWhiteSpace(documentFileName))
            {
                Text = label;
            }
            else
            {
                Text = $"{documentFileName}{(hasUnapplayedChanges ? '*' : ' ')} - {label}";
            }
            mainMenu.ForeColor = !hasUnapplayedChanges ? Color.Black : Color.Red;
        }
    }     
}