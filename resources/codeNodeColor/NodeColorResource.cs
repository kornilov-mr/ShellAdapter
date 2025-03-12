using System.IO;
using System.Text.Json;
using System.Windows.Media;
using ShellAdapter.logic.path;

namespace ShellAdapter.resources.codeNodeColor
{
    public class NodeColorResource
    {
        private static readonly string JsonPath =
            PathResolver.ResolvePathFromSolutionRoot("resources\\codeNodeColor\\NodeColorJson.json");
        private static Dictionary<string, Color> _resources = new();
        NodeColorResource()
        {

        }
        public static Color GetColor(string name)
        {
            if (_resources.Count==0)
            {
                LoadResources();
            }
            if (!_resources.ContainsKey(name))
            {
                return Color.FromRgb(0, 0, 0);
            
            }
            return _resources[name];
        }
        private static void LoadResources()
        {
            _resources = new Dictionary<string, Color>();
            List<ColorKeyValueToSerialize> o = JsonSerializer.Deserialize<List<ColorKeyValueToSerialize>>(File.ReadAllText(JsonPath));
            foreach (ColorKeyValueToSerialize keyValue in o)
            {
                _resources.Add(keyValue.name, keyValue.ColorRGB.ToColor());
            }
        }
    }
}
