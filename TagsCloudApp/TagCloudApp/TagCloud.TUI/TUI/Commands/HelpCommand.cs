using System;
using System.Linq;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name => "help";
        public string Description => "Help: use help <commandName> for detailed help";
        public Tuple<Result, Form> Perform(string command, string[] args, Engine engine)
        {
            if (args.Length == 0)
            {
                engine.Notify("Available commands:");
                foreach (var cmd in engine.CurrentForm.Commands)
                {
                    engine.Notify($"\t{cmd.Name}");
                }
            }
            else if (engine.CurrentForm.Commands.Any(c => c.Name == args.First()))
            {
                engine.Notify(engine.CurrentForm.Commands.First(c => c.Name == args.First()).Description);
            }
            else
            {
                engine.Notify($"Command `{args.First()}` is not founded:");
                engine.Notify($"\tuse `help` for list all commands");
            }
            return Tuple.Create(Result.Pass, (Form)null);
        }
    }
}