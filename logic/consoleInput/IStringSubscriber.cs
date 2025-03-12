namespace ShellAdapter.logic.consoleInput;

/// <summary>
/// Interface for observers for strings
/// </summary>
public interface IStringSubscriber
{
    public void ReportString(string? s);
}