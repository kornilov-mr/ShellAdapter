using NUnit.Framework;
using ShellAdapter.logic.consoleInput;

namespace ShellAdapter.tests;

[TestFixture]
public class ConsoleLinesMemoryTest
{
    [Test]
    public void EmptyMemoryTest1()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        Console.WriteLine(memory.GetPrevString());
        Assert.That(memory.GetPrevString(), Is.EqualTo(""));
    }

    [Test]
    public void EmptyMemoryTest2()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        Assert.That(memory.GetNextString(), Is.EqualTo(""));
    }

    [Test]
    public void GetPrevStringTest1()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        memory.ReportString("first command line");
        Assert.That(memory.GetPrevString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo(""));
    }

    [Test]
    public void GetPrevStringTest2()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        memory.ReportString("first command line");
        memory.ReportString("Second command line");
        Assert.That(memory.GetPrevString(), Is.EqualTo("Second command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo(""));
    }

    [Test]
    public void GetNextStringTest1()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        memory.ReportString("first command line");
        Assert.That(memory.GetPrevString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo(""));
        Assert.That(memory.GetNextString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetNextString(), Is.EqualTo(""));
    }

    [Test]
    public void GetNextStringTest2()
    {
        ConsoleLinesMemory memory = new ConsoleLinesMemory();
        memory.ReportString("first command line");
        memory.ReportString("Second command line");
        Assert.That(memory.GetPrevString(), Is.EqualTo("Second command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetPrevString(), Is.EqualTo(""));
        Assert.That(memory.GetNextString(), Is.EqualTo("first command line"));
        Assert.That(memory.GetNextString(), Is.EqualTo("Second command line"));
        Assert.That(memory.GetNextString(), Is.EqualTo(""));
    }
}