using System;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Tuple<Result, Form> Perform(string command, string[] args, Engine engine);
    }
}