﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.App.GUI.Actions;
using TagCloudApp.TagCloudRender;
using TagCloudApp.WordToTag;
using Utility.Geometry;

namespace TagCloudApp.App.GUI
{
    public class GuiApplication : Form, IApplication
    {

        public GuiApplication(IUiAction[] actions, PictureBox pictureBox, RenderSettings renderSettings, Dictionary<string, Rectangle> data)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            data.Add("A", new Rectangle(0, 0, 1200, 120));

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        public string RequestSavePath(string fileName, string extensions)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                DefaultExt = extensions
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

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }     
}