using System;
using System.Linq;
using System.Windows;

namespace TravelAgency.WindowHelpers
{
    public class WindowManager : IWindowManager
    {
        public void ShowWindow<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }

        public void CloseWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(T)) continue;
                window.Close();
                break;
            }
        }

        public Window? GetWindowFromViewModel(object viewModel)
        {
            var view = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == viewModel);
            return view;
        }

        public void MoveWindow(Window window, double deltaX, double deltaY)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            window.Left += deltaX;
            window.Top += deltaY;
        }
    }
}
