using System;
using System.Collections.Generic;

namespace TagCloudApp.TUI
{
    public class TuiForm
    {
        public readonly Dictionary<string, Func<string[], TuiForm>> Controls;
        public string Name { get; } = "TuiForm";

        public TuiForm()
        {
            Controls = new Dictionary<string, Func<string[], TuiForm>>();
        }

        public virtual void OnClose()
        { }

        public void Handle(string command, string[] args)
        {
            // CR: Can be replaced with polymorphism
            if (Controls.ContainsKey(command))
            {
                TuiEngine.Instance.PushToStack(Controls[command](args));
            }
            else
            {
                TuiEngine.Instance.Notify($"Command {command} is not founded", ConsoleColor.Red);
                TuiEngine.Instance.Notify($"Available commands:", ConsoleColor.Blue);
                TuiEngine.Instance.Notify("\tq\t\tfor return to previous form", ConsoleColor.DarkBlue);
                foreach (var control in Controls)
                {
                    TuiEngine.Instance.Notify($"\t{control.Key}", ConsoleColor.DarkBlue);
                }
            }
        }
    }
}