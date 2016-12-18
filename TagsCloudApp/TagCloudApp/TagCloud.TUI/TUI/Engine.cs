using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.TUI.TUI.Commands;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI.TUI
{
    public class Engine : IDisposable
    {
        public static Engine FromConsole(Form mainForm) => new Engine(Console.In, Console.Out, mainForm);

        private readonly TextReader reader;
        private readonly TextWriter writer;        
        private readonly Stack<Form> forms;
        private readonly MissCommand missCommand;

        public Form CurrentForm => forms.Peek();

        public Engine(TextReader reader, TextWriter writer, Form mainForm)
        {
            if (mainForm == null)
                throw new ArgumentNullException(nameof(mainForm));

            missCommand = new MissCommand();
            forms = new Stack<Form>();
            forms.Push(mainForm);
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            while (true)
            {
                var result = ExecuteLine(Read());
                switch (result.Item1)
                {
                    case Result.CloseApplication:
                        return;
                    case Result.CloseForm:
                        forms.Pop();
                        break;
                    case Result.OpenNewForm:
                        forms.Push(result.Item2);
                        break;
                }
                if (forms.Count == 0)
                    return;
            }
        }

        public void Notify(string message)
        {
            writer.WriteLine(message);
        }

        public string ReadRaw()
        {
            return reader.ReadLine() ?? String.Empty;
        }

        public string[] Read()
        {
            return ReadRaw().Trim('\n', '\r').Split('\t', ' ');
        }

        public void Dispose()
        {
            reader.Dispose();
            writer.Dispose();
        }

        private Tuple<Result, Form> ExecuteLine(string[] line)
        {
            var expectedCommand = line.FirstOrDefault();
            var args = line.Skip(1).ToArray();
            var commnad = CurrentForm.Commands.FirstOrDefault(c => c.Name == expectedCommand);
            if (commnad == null)
                return missCommand.Perform(expectedCommand, args, this);
            return commnad.Perform(expectedCommand, args, this);
        }
    }
}