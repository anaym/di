using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudApp.TUI
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
                var line = Console.ReadLine()?.Split(' ') ?? new string[0];
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
}