using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetEnv;
using ShellAdapter.logic.path;
using Path = System.IO.Path;

namespace ShellAdapter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        Env.Load(PathResolver.ResolvePathFromSolutionRoot(".env"));
        InitializeComponent();
    }

    public Object? FindByNameInAllContext(string name)
    {
        return FindName(name);
    }
}