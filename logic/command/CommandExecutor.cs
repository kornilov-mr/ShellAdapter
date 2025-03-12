using System.Diagnostics;
using System.IO;
using ShellAdapter.logic.consoleInput;

namespace ShellAdapter.logic.command;

/// <summary>
/// CommandExecutor, which is responsible for executing command
/// and handling virtual Working directory
/// </summary>
public class CommandExecutor
{
    /// <summary>
    /// Contains all observers for normal output from a command
    /// </summary>
    private List<IStringSubscriber> OutputSubscribers { get; } = new();
    /// <summary>
    /// Contains all observers for error output from a command
    /// </summary>
    private List<IStringSubscriber> ErrorSubscribers { get; } = new();
    /// <summary>
    /// Virtual working location for cmd
    /// </summary>
    public string CurrLocation { get; private set; }

    public CommandExecutor(string currentLocation)
    {
        CurrLocation = currentLocation;
    }

    public void AddOutputSubscriber(IStringSubscriber subscriber)
    {
        OutputSubscribers.Add(subscriber);
    }

    public void AddErrorSubscriber(IStringSubscriber subscriber)
    {
        ErrorSubscribers.Add(subscriber);
    }
    /// <summary>
    /// function, which executes command line
    /// </summary>
    /// <param name="command"> command line for cmd to execute</param>
    public void ExecuteCommand(string command)
    {
        if (string.IsNullOrEmpty(command)) return;
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/C {command}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        psi.WorkingDirectory = CurrLocation;
        using Process process = new Process();
        process.StartInfo = psi;
        process.EnableRaisingEvents = true;

        process.OutputDataReceived += (_, args) =>
        {
            foreach (IStringSubscriber printer in OutputSubscribers)
            {
                printer.ReportString(args.Data);
            }
        };
        process.ErrorDataReceived += (_, args) =>
        {
            foreach (IStringSubscriber printer in ErrorSubscribers)
            {
                printer.ReportString(args.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();
        if (command.Contains("cd") && !command.Equals("cd") && process.ExitCode == 0)
        {
            string newDirectory = command[3..].Trim();
            CurrLocation = Path.GetFullPath(Path.Combine(CurrLocation, newDirectory));
        }
    }
}