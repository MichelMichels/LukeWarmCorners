using LukeWarmCorners.Core.Native;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LukeWarmCorners.Core
{
    public partial class MouseTracker : IMouseTracker, IDisposable
    {
        private readonly Timer mousePollingTimer;
        private POINT lastPosition;

        public MouseTracker(int pollIntervalInMs)
        {
            mousePollingTimer = new Timer(state => ProcessMousePosition(), null, 0, pollIntervalInMs);
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetCursorPos(out POINT pPoint);

        private void ProcessMousePosition()
        {
            GetCursorPos(out POINT mouse);

            if(!mouse.Equals(lastPosition))
            {
                MouseMoved?.Invoke(this, new Point(mouse.X, mouse.Y));
                lastPosition = mouse;
            }            
        }

        public void Dispose()
        {
            mousePollingTimer.Dispose();
            GC.SuppressFinalize(this);
        }

        public Point GetCurrentPosition()
        {
            GetCursorPos(out POINT mouse);

            return new Point(mouse.X, mouse.Y);
        }

        public event EventHandler<Point>? MouseMoved;
    }
}
