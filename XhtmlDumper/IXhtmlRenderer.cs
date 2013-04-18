using System.IO;
using System.Web.UI;

namespace XhtmlDumper
{
    public interface IXhtmlRenderer
    {
        bool Render(object o, string description, int depth, XhtmlTextWriter writer);
    }
}
