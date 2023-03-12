using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        public SignInView()
        {
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

        private void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                ErrorMessage.Text = "You haven't entered the username.";
                UsernameTextBox.Focus();
            }

            else if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                ErrorMessage.Text = "You haven't entered the password.";
                PasswordBox.Focus();
            }

            else
            {
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;

                if (username.Length < 3 || password.Length < 3)
                {
                    ErrorMessage.Text = "Username or password is too short.";
                }

                else
                {
                    ErrorMessage.Text = "";
                    IUserRepository userRepository = new UserRepository();
                    var isValid = userRepository.AuthenticateUser(new System.Net.NetworkCredential(username, password));
                    if (isValid)
                    {
                        MainView mainView = new MainView();
                        mainView.Show();
                        Close();
                    }
                    else
                    {
                        ErrorMessage.Text = "Incorrect username or password.";
                    }
                }
            }
        }
    }
}
