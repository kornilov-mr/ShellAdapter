using System.Windows.Controls;
using System.Windows.Input;
using ShellAdapter.logic.command;
using ShellAdapter.logic.consoleInput;
using ShellAdapter.logic.consoleOutput;
using ShellAdapter.logic.document;
using ShellAdapter.logic.path;

namespace ShellAdapter.UiComponents;

public class ConsoleInputTextBox : TextBox
{
    public string ErrorPrinterName { get; set; } = null!;
    public string OutputPrinterName { get; set; } = null!;

    private ErrorPrinter ErrorPrinterInstance { get; set; } = null!;
    private OutputPrinter OutputPrinterInstance { get; set; } = null!;

    private CommandExecutor CommandExecutor { get; } = new CommandExecutor(
        PathResolver.ResolvePathFromSolutionRoot(
            Environment.GetEnvironmentVariable("STARTING_FOLDER")));
    private ConsoleLinesMemory Memory { get; } = new ConsoleLinesMemory();
    private string _lastValidText = "";
    private ConsoleDocument Document { get; set; } = null!;


    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        foreach (var change in e.Changes)
        {
            int lineIndex = Document.GetLineBasedOnPosition(change.Offset);
            if (lineIndex != Document.GetLineCount() - 1)
            {
                Text = _lastValidText; 
                return;
            }
        }
        _lastValidText = Text;
        Document = new ConsoleDocument(this.Text);
        base.OnTextChanged(e);
    }

    private void LoadPrinterInstances()
    {
        ErrorPrinterInstance = (ErrorPrinter)this.FindName(ErrorPrinterName) ??
                               throw new InvalidOperationException($"Can't find {ErrorPrinterName}");
        OutputPrinterInstance = (OutputPrinter)this.FindName(OutputPrinterName) ??
                                throw new InvalidOperationException($"Can't find {OutputPrinterName}");
        CommandExecutor.AddErrorSubscriber(ErrorPrinterInstance);
        CommandExecutor.AddOutputSubscriber(OutputPrinterInstance);
    }
    
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        string s;
        switch (e.Key)
        {
            case Key.Enter:
                Memory.ReportString(Document.GetLastString());
                CommandExecutor.ExecuteCommand(Document.GetLastString());
                AppendText("\n" + CommandExecutor.CurrLocation+" ");
                CaretIndex=Text.Length;
                Document = new ConsoleDocument(this.Text);
                e.Handled = true;
                break;
            case Key.Up:
                s = Memory.GetPrevString();
                AppendText(s);
                e.Handled = true;
                break;
            case Key.Down:
                s = Memory.GetNextString();
                AppendText(s);
                e.Handled = true;
                break;
        }
        
        base.OnPreviewKeyDown(e);
    }

    public override void EndInit()
    {
        LoadPrinterInstances();
        Document = new ConsoleDocument(CommandExecutor.CurrLocation+" ");
        Text = Document.Text;
        base.EndInit();
    }
}