using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for RenovationRequestView.xaml
    /// </summary>
    public partial class RenovationRequestView : Window
    {
        private readonly RenovationRequestViewModel _viewModel = new();
        public RenovationRequestView(int acc_id)
        {
            _viewModel.AccId = acc_id;
            DataContext = _viewModel;
            InitializeComponent();
        }
        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SubmitRenovation();
            MessageBox.Show("Success!");
        }
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
