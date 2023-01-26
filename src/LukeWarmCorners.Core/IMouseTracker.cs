using System.Drawing;

namespace LukeWarmCorners.Core
{
    public interface IMouseTracker
    {
        event EventHandler<Point>? MouseMoved;

        Point GetCurrentPosition();
    }
}