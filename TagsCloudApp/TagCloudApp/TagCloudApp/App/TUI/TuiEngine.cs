using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudApp.App.TUI
{
    public class TuiEngine
    {
        public static readonly TuiEngine Instance = new TuiEngine();

        private readonly Stack<TuiForm> Frames;
        private TuiEngine()
        {
            Frames = new Stack<TuiForm>();
        }

        public void Run(TuiForm form)
        {
            PushToStack(form);
            while (true)
            {
                // CR: Resharper warning makes sense
                var line = Console.ReadLine().Split(' ');
                var command = line.Length > 0 ? line[0] : string.Empty;
                // CR: Can be replaced with polymorphism
                if (command == "q" || command == "quit")
                { 
                    CloseCurrent();
                }
                else
                {
                    CurrentForm?.Handle(command, line.Skip(1).ToArray());
                }
            }
        }

        public TuiForm CurrentForm => Frames.Count == 0 ? null : Frames.Peek();
        public void PushToStack(TuiForm form)
        {
            Frames.Push(form);
            Console.Title = CurrentForm?.Name ?? "";
        }

        private void CloseCurrent()
        {
            CurrentForm?.OnClose();
            if (Frames.Count > 1)
            { 
                Frames.Pop();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void Notify(string line, ConsoleColor color = ConsoleColor.White)
        {
            Console.WriteLine(line, color);
        }
    }

    // CR: 1 class = 1 file
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