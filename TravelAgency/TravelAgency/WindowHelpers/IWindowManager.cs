using System.Windows;
using TravelAgency.View.Tourist;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.WindowHelpers
{
    public interface IWindowManager
    {
        void ShowWindow<T>() where T : Window, new();
        void CloseWindow<T>() where T : Window;
        T GetWindow<T>() where T : Window, new();
    }
}
