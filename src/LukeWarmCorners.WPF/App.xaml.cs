using LukeWarmCorners.Core;
using System.Windows;
using WindowsInputCore;

namespace LukeWarmCorners.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow()
            {
                DataContext = new MainViewModel(new MouseTracker(100), new InputSimulator())
            };
            window.Show();

            base.OnStartup(e);
        }
    }
}
