
using System.Windows.Media;

namespace ShellAdapter.resources.codeNodeColor
{
    internal class ColorToSerialize
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public Color ToColor()
        {
            var color = System.Drawing.Color.FromArgb(R, G, B);
            return Color.FromRgb(color.R,color.G,color.G);
        }
    }
}
