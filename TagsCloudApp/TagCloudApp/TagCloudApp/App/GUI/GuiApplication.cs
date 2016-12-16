using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.App.Actions;
using TagCloudApp.Renderer;
using Utility.Geometry;

namespace TagCloudApp.App.GUI
{
    public class GuiApplication : Form, IApplication
    {
        private readonly string label;
        private string fileName;
        private bool isNewFile;
        private readonly MenuStrip mainMenu;

        public GuiApplication(IUiAction[] actions, PictureBox pictureBox, RendererSettings rendererSettings, TagCollection collection)
        {
            label = "Tag Cloud Layouter";
            fileName = "";
            isNewFile = false;

            mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems(this));
            mainMenu.Dock = DockStyle.Bottom;
            Controls.Add(mainMenu);

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            ChangeDocumentStatus();
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        #region Utility
        public void ChangeDocumentNewStatus(bool @new)
        {
            isNewFile = @new;
            ChangeDocumentStatus();

        }

        public void ChangeDocumentFileName(string name)
        {
            fileName = name;
            ChangeDocumentStatus();
        }
        private void ChangeDocumentStatus()
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Text = label;
            }
            else
            {
                Text = $"{fileName}{(isNewFile ? '*' : ' ')} - {label}";
            }
            mainMenu.ForeColor = !isNewFile ? Color.Black : Color.Red;
        }

        public void Notify(string message)
        {
            MessageBox.Show(message);
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