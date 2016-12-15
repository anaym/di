using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
                var line = Console.ReadLine().Split(' ');
                var command = line.Length > 0 ? line[0] : "";
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
                Process.GetCurrentProcess().Kill();
            }
        }

        public void Notify(string line, ConsoleColor color = ConsoleColor.White)
        {
            Console.WriteLine(line, color);
        }
    }

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