using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ShellAdapter.logic.consoleOutput;
using ShellAdapter.resources.codeNodeColor;

namespace ShellAdapter.UiComponents;

public class ConsoleOutputTextBox:RichTextBox
{
    private Paragraph _paragraph = new Paragraph();
    public void DisplayNewString(string text, TextTagEnum textTag)
    {
        if (String.IsNullOrEmpty(text)) return;
        
        Run run = new Run(text+"\n");
        switch (textTag)
        {
            case TextTagEnum.Normal:
                run.Foreground=new SolidColorBrush(NodeColorResource.GetColor("NormalHighLightColor"));
                break;
            case TextTagEnum.Error:
                run.Foreground= new SolidColorBrush(NodeColorResource.GetColor("ErrorHighlightColor"));
                break;
        }
        
        _paragraph.Inlines.Add(run);
        ScrollToEnd();
    }

    public override void EndInit()
    {
        Document.Blocks.Add(_paragraph);
        base.EndInit();
    }
}