using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.App.Actions;
using TagCloudApp.Renderer;
using Utility.Geometry;

namespace TagCloudApp.App.GUI
{
    public class GuiApplication : Form, IApplication
    {

        public GuiApplication(IUiAction[] actions, PictureBox pictureBox, RenderSettings renderSettings, TagCollection collection)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems(this));
            mainMenu.Dock = DockStyle.Bottom;
            Controls.Add(mainMenu);

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        #region Utility
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