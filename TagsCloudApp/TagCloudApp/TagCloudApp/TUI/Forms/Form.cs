using System.Collections.Generic;
using TagCloudApp.TUI.Commands;

namespace TagCloudApp.TUI.Forms
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