using System.Collections.Generic;
using TagCloud.TUI.TUI.Commands;

namespace TagCloud.TUI.TUI.Forms
{
    public class Form
    {
        public readonly List<ICommand> Commands;

        public Form()
        {
            Commands = new List<ICommand> {new EscapeCommand(), new HelpCommand()};
        }

    }

    class FileOpenForm : Form
    {
    }
}