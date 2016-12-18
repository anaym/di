using System;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI.Commands
{
    internal sealed class MissCommand : ICommand
    {
        public string Name { get; } = null;
        public string Description { get; } = null;
        public Tuple<Result, Form> Perform(string command, string[] args, Engine engine)
        {
            engine.Notify($"Command `{command}` is not supported!");
            return Tuple.Create(Result.Pass, (Form)null);
        }
    }
}