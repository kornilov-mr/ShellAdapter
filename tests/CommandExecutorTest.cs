using DotNetEnv;
using NUnit.Framework;
using ShellAdapter.logic.command;
using ShellAdapter.logic.consoleOutput;
using ShellAdapter.logic.path;

namespace ShellAdapter.tests;

[TestFixture]
public class CommandExecutorTest
{
    [Test]
    public void NonExceptionExecutorTest()
    {
        Env.Load(PathResolver.ResolvePathFromSolutionRoot(".env"));
        TestPrinter normalPrinter = new TestPrinter();
        TestPrinter exceptionPrinter = new TestPrinter();
        CommandExecutor commandExecutor = new CommandExecutor(
            PathResolver.ResolvePathFromSolutionRoot(
                Environment.GetEnvironmentVariable("STARTING_FOLDER")));
        commandExecutor.AddErrorSubscriber(exceptionPrinter);
        commandExecutor.AddOutputSubscriber(normalPrinter);
        commandExecutor.ExecuteCommand("type CatTest");
        Assert.That(normalPrinter.AllText, Is.EqualTo("Test text\n"));
        Assert.That(exceptionPrinter.AllText, Is.EqualTo(""));
    }
    [Test]
    public void WithExceptionExecutorTest()
    {
        Env.Load(PathResolver.ResolvePathFromSolutionRoot(".env"));
        TestPrinter normalPrinter = new TestPrinter();
        TestPrinter exceptionPrinter = new TestPrinter();
        CommandExecutor commandExecutor = new CommandExecutor(
            PathResolver.ResolvePathFromSolutionRoot(
                Environment.GetEnvironmentVariable("STARTING_FOLDER")));
        commandExecutor.AddErrorSubscriber(exceptionPrinter);
        commandExecutor.AddOutputSubscriber(normalPrinter);
        commandExecutor.ExecuteCommand("cd NonExistentPath");
        Assert.That(normalPrinter.AllText, Is.EqualTo(""));
        Assert.That(exceptionPrinter.AllText, Is.EqualTo("The system cannot find the path specified.\n"));
    }
}