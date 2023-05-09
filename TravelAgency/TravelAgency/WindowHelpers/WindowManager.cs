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

        public T GetWindow<T>() where T : Window, new()
        {
            foreach (var window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(T)) continue;
                return (T)window;
            }

            return new T();
        }
    }
}
