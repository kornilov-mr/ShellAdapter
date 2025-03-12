using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Legacy;
using ShellAdapter.logic.document;

namespace ShellAdapter.tests;

[TestFixture]
public class ConsoleDocumentTest
{
    private static readonly string Text = "Hello World\n" +
                                  "Example Text";
    private static readonly string CommandLineRight = "C:\\Users\\ThinkPad\\Documents cd";
    private static readonly string CommandLineFalse = "C:\\Users\\ThinkPad\\Documentscd";
    [Test]
    public void LengthOfLineTest()
    {
        ConsoleDocument document = new ConsoleDocument(Text);
        CollectionAssert.AreEqual(new List<int>{12, 13},document.LengthOfLine);
    }
    [Test]
    public void OffsetOnStartLineTest()
    {
        ConsoleDocument document = new ConsoleDocument(Text);
        CollectionAssert.AreEqual(new List<int>{0, 12, 25},document.OffsetOnStartLine);
    }
    [Test]
    public void GetLastLineTest1()
    {
        ConsoleDocument document = new ConsoleDocument(CommandLineRight);
        Assert.That(document.GetLastString(), Is.EqualTo("cd"));
    }
    [Test]
    public void GetLastLineTest2()
    {
        ConsoleDocument document = new ConsoleDocument(CommandLineFalse);
        Assert.That(document.GetLastString(), Is.EqualTo(""));
    }
}