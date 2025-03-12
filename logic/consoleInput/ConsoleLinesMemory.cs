namespace ShellAdapter.logic.consoleInput;

/// <summary>
/// Class, which remember all previous commands
/// </summary>
public class ConsoleLinesMemory : IStringSubscriber
{
    private List<string> StringsInMemory { get; } = new List<string>();
    private int _currentStringIndex = -1;

    public void ReportString(string? s)
    {
        if (String.IsNullOrEmpty(s)) return;
        StringsInMemory.Add(s);
        _currentStringIndex = StringsInMemory.Count;
    }
    
    public string GetPrevString()
    {
        if (_currentStringIndex >= 0) _currentStringIndex--;
        return GetStringSelected(_currentStringIndex);
    }

    public string GetNextString()
    {
        if (_currentStringIndex < StringsInMemory.Count) _currentStringIndex++;
        return GetStringSelected(_currentStringIndex);
    }

    private string GetStringSelected(int index)
    {
        if (index <= -1 || index >= StringsInMemory.Count) return "";
        return StringsInMemory[index];
    }
}