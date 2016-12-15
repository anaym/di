using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagCloudApp.App.GUI.Actions
{
	public static class UiActionExtensions
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
			var menuItems = items.Select(a => a.ToMenuItem(app)).ToArray();
			return new ToolStripMenuItem(name, null, menuItems);
		}

	    public static ToolStripItem ToMenuItem(this IUiAction action, IApplication app)
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