using ShellAdapter.logic.consoleInput;

namespace ShellAdapter.logic.consoleOutput;

public class TestPrinter : IStringSubscriber
{
    public string AllText { get; private set; }="";

    public void ReportString(string? s)
    {
        if (String.IsNullOrEmpty(s)) return;
        AllText += s+"\n";
    }
}