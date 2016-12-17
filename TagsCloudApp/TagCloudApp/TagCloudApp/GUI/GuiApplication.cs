using System.Drawing;
using System.Windows.Forms;
using TagCloudApp.Actions;

namespace TagCloudApp.GUI
{
    public class GuiApplication : Form, IApplication
    {
        private readonly string label;
        private string documentFileName;
        private bool hasUnapplayedChanges;
        private readonly MenuStrip mainMenu;

        public GuiApplication(IUiAction[] actions, PictureBox pictureBox)
        {
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(512, 512);

            label = "Tag Cloud Layouter";
            documentFileName = "";
            hasUnapplayedChanges = false;

            mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems(this));
            mainMenu.Dock = DockStyle.Bottom;
            Controls.Add(mainMenu);

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            UpdateTitle();
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        #region Utility
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

        public void Notify(string comment, string title="Attention!")
        {
            MessageBox.Show(comment, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public string RequestSavePath(string fileName="", string extensions="")
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                DefaultExt = extensions,
                FileName = fileName,
                Filter = $"{extensions.TrimStart('.')}|.{extensions.TrimStart('.')}"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }

        public string[] RequestOpenFiles(string extensions)
        {
            var dialog = new OpenFileDialog
            {
                CheckPathExists = true,
                Multiselect = true,
                DefaultExt = extensions
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileNames;
            }
            return null;
        }

        public string RequestOpenFolders()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }

        public void Request<T>(T settingsObject)
        {
            SettingsForm.For(settingsObject).ShowDialog();
        }

        #endregion
    }     
}