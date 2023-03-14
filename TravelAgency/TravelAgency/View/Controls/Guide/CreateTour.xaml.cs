using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        public CreateTour()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern nint SendMessage(nint wnd, int wMsg, nint wParam, nint lParam);

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
        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new GuideView();
            guideView.Show();
            Close();
        }

        private void CreateTour_OnClick(object sender, RoutedEventArgs e)
        {
            var createTour = new CreateTour();
            createTour.Show();
            Close();
        }

        private void MonitorTour_OnClick(object sender, RoutedEventArgs e)
        {
            var monitorTour = new MonitorTour();
            monitorTour.Show();
            Close();
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameText.Text.Length < 3)
            {
                ErrorMessageText.Text = "Incorrect name.";
                NameText.Focus();
            }
            else
            {
                if (ComboBoxLocation.Text.Length == 0)
                {
                    ErrorMessageText.Text = "Insert a location.";
                    ComboBoxLocation.Focus();
                }
                else
                {
                    if (ComboBoxLanguage.Text.Length == 0)
                    {
                        ErrorMessageText.Text = "Insert a language.";
                        ComboBoxLanguage.Focus();
                    }
                    else
                    {
                        if (MaxGuestsText.Text.Length == 0)
                        {
                            ErrorMessageText.Text = "Insert a number";
                            MaxGuestsText.Focus();
                        }
                        else if (!Regex.IsMatch(MaxGuestsText.Text, @"^[0-9]+$"))
                        {
                            ErrorMessageText.Text = "You can only insert numbers.";
                            MaxGuestsText.Focus();
                        }
                        else
                        {
                            if (CheckBoxKeyPoints.Text.Length == 0)
                            {
                                ErrorMessageText.Text = "Insert at least one key point.";
                                CheckBoxKeyPoints.Focus();
                            }
                            else
                            {
                                if (DateList.Text.Length == 0)
                                {
                                    ErrorMessageText.Text = "Insert at least one date.";
                                    DatePick.Focus();
                                }
                                else
                                {
                                    if (DurationText.Text.Length == 0)
                                    {
                                        ErrorMessageText.Text = "Insert a number.";
                                        DurationText.Focus();
                                    }
                                    else if (!Regex.IsMatch(DurationText.Text, @"([0-9]*[.])?[0-9]+$"))
                                    {
                                        ErrorMessageText.Text = "You can only insert a number.";
                                        DurationText.Focus();
                                    }
                                    else
                                    {
                                        ErrorMessageText.Text = "";
                                        var createTour = new CreateTour();
                                        createTour.Show();
                                        Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddNewDate_OnClick(object sender, MouseButtonEventArgs e)
        {
            bool hasText = false;
            if (DateList.Text.Contains(DatePick.Text))
            {
                DateList.Text.Replace(DatePick.Text, "");
                hasText = true;
            }

            if (DateList.Text.Contains(",,"))
                DateList.Text.Replace(",,", ",");
            if (DateList.Text.StartsWith(","))
                DateList.Text.Remove(0, 1);
            if(!hasText)
                if(DateList.Text.Length == 0)
                    DateList.Text += DatePick.Text;
                else
                    DateList.Text += "," + DatePick.Text;
            DatePick.Text = "";
        }

        private void DeleteDates_OnClick(object sender, RoutedEventArgs e)
        {
            DateList.Text = "";
        }
    }
}
