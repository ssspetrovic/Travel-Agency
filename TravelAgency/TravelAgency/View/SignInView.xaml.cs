﻿using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View.Tourist;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInView
    {
        public SignInView()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
            CurrentLanguageAndTheme.languageId = 0; //English default
            CurrentLanguageAndTheme.themeId = 0; //Light default
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
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
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;

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
                        var currentRole = userRepository.GetRole(username);
                        CurrentUser.Id = userRepository.GetByUsername(username).Id;
                        CurrentUser.Username = username;
                        CurrentUser.DisplayName = userRepository.GetByUsername(username).Name;

                        switch (currentRole)
                        {
                            case Role.Owner:
                                {
                                    var mainView = new OwnerMainView();
                                    mainView.Show();
                                    break;
                                }
                            case Role.Guide:
                                {
                                    var guideView = new Guide();
                                    guideView.Show();
                                    break;
                                }
                            case Role.Guest1:
                                {
                                    var guest1View = new Guest1View();
                                    guest1View.Show();
                                    break;
                                }
                            case Role.Tourist:
                            default:
                                {
                                    var touristView = new WizardDialogView();
                                    touristView.Show();
                                    break;
                                }
                        }
                        Close();
                    }
                    else
                    {
                        ErrorMessage.Text = "Incorrect username or password.";
                    }
                }
            }
        }

        private void UsernamePassword_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignInButton_OnClick(sender, e);
            }
        }
    }
}
