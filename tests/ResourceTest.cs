using System.Windows.Media;
using NUnit.Framework;
using ShellAdapter.resources.codeNodeColor;

namespace ShellAdapter.tests;
[TestFixture]
public class ResourceTest
{
    [Test]
    public void TestResourceLoad()
    {
        Color color = NodeColorResource.GetColor("NormalHighLightColo");
        Assert.That(color, Is.Not.Null);
    }
}