using System.Windows;

namespace TravelAgency.WindowHelpers
{
    public interface IWindowManager
    {
        void ShowWindow<T>() where T : Window, new();
        void CloseWindow<T>() where T : Window;
        Window? GetWindowFromViewModel(object viewModel);
        void MoveWindow(Window window, double deltaX, double deltaY);
    }
}
