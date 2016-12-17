using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloudApp.Actions;

namespace TagCloudApp.GUI
{
	public static class GuiActionExtensions
	{
		public static ToolStripItem[] ToMenuItems(this IUiAction[] actions, IApplication app)
		{
			var items = actions
                .OrderBy(a => a.Index)
                .ThenBy(a => a.Category)
                .GroupBy(a => a.Category)
                .OrderBy(g => g.First().Index)
				.Select(g => CreateToplevelMenuItem(g.Key, g.ToList(), app))
				.Cast<ToolStripItem>()
				.ToArray();
			return items;
		}

		private static ToolStripMenuItem CreateToplevelMenuItem(string name, IList<IUiAction> items, IApplication app)
		{
		    if (items.Count == 1 && name == items.First().Name)
		    {
		        return items.First().ToMenuItem(app);
		    }
			var menuItems = items.Select(a => a.ToMenuItem(app)).ToArray();
			return new ToolStripMenuItem(name, null, menuItems);
		}

	    public static ToolStripMenuItem ToMenuItem(this IUiAction action, IApplication app)
	    {
	        return
	            new ToolStripMenuItem(action.Name, null, (sender, args) => action.Perform(app))
	            {
	                ToolTipText = action.Description,
	                Tag = action
	            };
	    }
	}
}