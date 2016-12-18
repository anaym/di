using System;
using TagCloud.TUI.TUI;
using TagCloud.TUI.TUI.Commands;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var form = new Form();
            var load = new StringRequestCommand("load", "load file", "path:");
            load.OnEnter += Console.WriteLine;
            form.Commands.Add(load);
            Engine.FromConsole(form).Run();
        }
    }
}
