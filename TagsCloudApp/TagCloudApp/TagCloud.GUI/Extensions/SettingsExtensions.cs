using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Settings;
using Utility.RailwayExceptions;

namespace TagCloud.GUI.Extensions
{
	public static class SettingsExtensions
	{
		public static ToolStripItem[] ToMenuItems(this ISettings[] settings, string category)
		{
			var items = settings
                .OrderBy(g => g.GetSettingsName())
                .GroupBy(a => 0)
				.Select(g => CreateToplevelMenuItem(category, g.ToList()))
				.Cast<ToolStripItem>()
				.ToArray();
			return items;
		}

		private static ToolStripMenuItem CreateToplevelMenuItem(string name, IList<ISettings> items)
		{
		    if (items.Count == 1 && name == items.First().GetSettingsName())
		    {
		        return items.First().ToMenuItem();
		    }
			var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
			return new ToolStripMenuItem(name, null, menuItems);
		}

	    public static ToolStripMenuItem ToMenuItem(this ISettings settings)
	    {
	        return new ToolStripMenuItem(settings.GetSettingsName(), null, (sender, args) => settings.RequestSetup())
	            {
	                ToolTipText = $"Settings for {settings.GetSettingsName()}",
	                Tag = settings
	            };
	    }

	    public static void RequestSetup(this ISettings settings)
	    {
	        SettingsForm.For(settings).ShowDialog().OnExceptionNotify();
	    }
	}
}