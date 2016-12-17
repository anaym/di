using System;
using TagCloudApp.TUI.Forms;

namespace TagCloudApp.TUI.Commands
{
    public class EscapeCommand : ICommand
    {
        public string Name => "escape";
        public string Description => "Esacape from this form";
        public Tuple<Result, Form> Perform(string command, string[] args, Engine engine)
        {
            return Tuple.Create(Result.CloseForm, (Form) null);
        }
    }
}