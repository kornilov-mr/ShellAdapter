namespace ShellAdapter.logic.document;

public class ConsoleDocument : Document
{
    public string Text { get; }
    private string[] Strings { get; set; } = null!;

    public ConsoleDocument(string text)
    {
        Text = text;
        CountLengthOfLine();
        CalculateOffsetOnStartLineFromCharOnLine();
    }

    private void CountLengthOfLine()
    {
        Strings = Text.Split("\n");
        foreach (var s in Strings)
        {
            LengthOfLine.Add(s.Length+1);
        }
    }

    public string GetLastString()
    {
        if (!Strings[^1].Contains(" ")) return "";
        return Strings[^1].Substring(Strings[^1].IndexOf(" ")+1);
    }
}