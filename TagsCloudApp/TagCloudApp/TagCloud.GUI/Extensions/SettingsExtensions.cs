using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Settings;

namespace TagCloud.GUI.Extensions
{
	public static class SettingsExtensions
	{
		public static ToolStripItem[] ToMenuItems(this ISettings[] actions, string category)
		{
			var items = actions
                .GroupBy(a => 0)
                .OrderBy(g => g.First().SettingsName)
				.Select(g => CreateToplevelMenuItem(category, g.ToList()))
				.Cast<ToolStripItem>()
				.ToArray();
			return items;
		}

		private static ToolStripMenuItem CreateToplevelMenuItem(string name, IList<ISettings> items)
		{
		    if (items.Count == 1 && name == items.First().SettingsName)
		    {
		        return items.First().ToMenuItem();
		    }
			var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
			return new ToolStripMenuItem(name, null, menuItems);
		}

	    public static ToolStripMenuItem ToMenuItem(this ISettings settings)
	    {
	        return new ToolStripMenuItem(settings.SettingsName, null, (sender, args) => settings.RequestSetup())
	            {
	                ToolTipText = $"Settings for {settings.SettingsName}",
	                Tag = settings
	            };
	    }

	    public static void RequestSetup(this ISettings settings)
	    {
	        SettingsForm.For(settings).ShowDialog();
	    }
	}
}