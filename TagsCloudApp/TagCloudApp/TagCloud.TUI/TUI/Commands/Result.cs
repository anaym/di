namespace TagCloud.TUI.TUI.Commands
{
    public enum Result
    {
        CloseApplication = 0,
        CloseForm = 1 << 0,
        OpenNewForm = 1 << 1,
        Pass = 1 << 2
    }
}