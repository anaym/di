using TagCloud.TUI.TUI;
using TagCloud.TUI.TUI.Forms;

namespace TagCloud.TUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var form = new Form();
            Engine.FromConsole(form).Run();
        }
    }
}
