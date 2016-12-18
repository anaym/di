using System;
using System.Windows.Forms;

namespace TagCloud.GUI.Extensions
{
    public static class MenuExtensions
    {
        public static ToolStripMenuItem ConnectCategory(this MenuStrip menu, string name, Action action = null, string description = "")
        {
            var item = new ToolStripMenuItem(name, null, (sender, args) => action?.Invoke())
            {
                ToolTipText = description,
                Tag = name
            };
            menu.Items.Add(item);
            return item;
        }

        public static ToolStripMenuItem Connect(this ToolStripMenuItem parent, string name, Action action = null, string description = "")
        {
            var item = new ToolStripMenuItem(name, null, (sender, args) => action?.Invoke())
            {
                ToolTipText = description,
                Tag = name
            };
            parent.DropDownItems.Add(item);
            return parent;
        }

        public static ToolStripMenuItem ConnectSeparator(this ToolStripMenuItem parent)
        {
            parent.DropDownItems.Add(new ToolStripSeparator());
            return parent;
        }
    }
}