using System.Drawing;

namespace TagCloud.Core.Task
{
    public interface ITagLayoutTask
    {
        Bitmap Solve();
    }
}