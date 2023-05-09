﻿using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace TravelAgency.View
{
    public partial class Guide
    {
        public Guide()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]
        public static extern nint SendMessage(nint wnd,  int wMsg, nint wParam, nint lParam);

        private void PanelControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void PanelControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Button_CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_MaximizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Button_MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
