using System.Windows;
using ShellAdapter.logic.consoleInput;
using ShellAdapter.UiComponents;

namespace ShellAdapter.logic.consoleOutput;

public class AbstractPrinter : FrameworkElement, IStringSubscriber
{
    
    public string NameOfBlockToWrite { get; set; } = null!;
    private ConsoleOutputTextBox OutputBox { get; set; } = null!;
    private TextTagEnum TextTag { get; }

    protected AbstractPrinter(TextTagEnum textTag)
    {
        this.TextTag = textTag;
    }

   

    protected override void OnInitialized(EventArgs e)
    {
        
        
    }

    public void ReportString(string? s)
    {
        if (String.IsNullOrEmpty(s)) return;
        Dispatcher.BeginInvoke(
            new Action(() => {
                OutputBox.DisplayNewString(s, TextTag);
            })
        );
    }

    public override void EndInit()
    {
        OutputBox = (ConsoleOutputTextBox)this.FindName(NameOfBlockToWrite) ??
                    throw new InvalidOperationException($"Can't find {NameOfBlockToWrite}");
    }
}