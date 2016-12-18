using System;
using System.Linq;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI.Commands
{
    internal sealed class MissCommand : ICommand
    {
        public string Name { get; } = null;
        public string Description { get; } = null;
        public Tuple<Result, Form> Perform(string command, string[] args, Engine engine)
        {
            var help = engine.CurrentForm.Commands.FirstOrDefault(c => c is HelpCommand) as HelpCommand;
            engine.Notify($"Command `{command}` is not supported! {(help != null ? $"Use `{help.Name}` for list of all commands" : "")}");
            return Tuple.Create(Result.Pass, (Form)null);
        }
    }
}