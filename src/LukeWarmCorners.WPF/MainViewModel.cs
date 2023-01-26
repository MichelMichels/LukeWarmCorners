using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LukeWarmCorners.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;
using WindowsInputCore;
using WindowsInputCore.Native;

namespace LukeWarmCorners.WPF
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly List<Point> leftCorners = new List<Point>()
        {
            new Point(0, 0),
            new Point(2400, -970),
            new Point(5600, -1348),
        };

        private readonly IMouseTracker mouseTracker;
        private readonly IInputSimulator inputSimulator;

        private bool wasInLeftCorner = false;

        [ObservableProperty]
        private Point currentMousePosition;

        public MainViewModel(IMouseTracker mouseTracker, IInputSimulator inputSimulator)
        {
            this.mouseTracker = mouseTracker ?? throw new ArgumentNullException(nameof(mouseTracker));
            this.inputSimulator = inputSimulator ?? throw new ArgumentNullException(nameof(inputSimulator));

            mouseTracker.MouseMoved += MouseTracker_MouseMoved;
        }        

        [RelayCommand]
        public void WriteCurrentPosition()
        {
            var mouse = mouseTracker.GetCurrentPosition();
            CurrentMousePosition = new Point(mouse.X, mouse.Y);
        }

        private void MouseTracker_MouseMoved(object? sender, Point e)
        {
            if (IsCurrentlyInLeftCorner(e) && !wasInLeftCorner)
            {
                SimulateWindowsTabKeyPress();
                wasInLeftCorner = true;
            }
            else
            {
                wasInLeftCorner = false;
            }
        }

        private bool IsCurrentlyInLeftCorner(Point point)
        {
            foreach(var corner in leftCorners)
            {
                if (point.X >= corner.X && point.X <= corner.X + 5 &&
                    point.Y >= corner.Y && point.Y <= corner.Y + 5) {
                    return true;
                }                
            }

            return false;
        }

        private void SimulateWindowsTabKeyPress()
        {
            try
            {
                inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
            }
            catch
            {
            }
        }
    }
}
