using System;
using TagCloudApp.TUI.Forms;

namespace TagCloudApp.TUI.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Tuple<Result, Form> Perform(string command, string[] args, Engine engine);
    }
}