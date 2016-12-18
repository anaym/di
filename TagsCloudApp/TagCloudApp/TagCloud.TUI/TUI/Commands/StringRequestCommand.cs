using System;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI.Commands
{
    public class StringRequestCommand : ICommand
    {
        private readonly string requestTitle;

        public StringRequestCommand(string name, string description, string requestTitle)
        {
            this.requestTitle = requestTitle;
            Name = name;
            Description = description;
            OnEnter = s => { };
        }

        public string Line { get; set; }
        public string Name { get; }
        public string Description { get; }
        public event Action<string> OnEnter;
        public Tuple<Result, Form> Perform(string command, string[] args, Engine engine)
        {
            engine.Notify($"\t{requestTitle}");
            Line = engine.ReadRaw();
            OnEnter.Invoke(Line);
            return Tuple.Create(Result.Pass, (Form) null);
        }
    }
}