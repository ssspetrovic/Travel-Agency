﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Model;
using TravelAgency.Repository;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CurrentTour.xaml
    /// </summary>
    public partial class CurrentActiveTour
    {
        public CurrentActiveTour()
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

        private void MonitorTour_OnClick(object sender, RoutedEventArgs e)
        {
            var monitorTour = new MonitorTour();
            monitorTour.Show();
            Close();
        }

        private void ChangeCurrentKeyPoint_OnClick(object sender, RoutedEventArgs e)
        {
            var keyPoint = (string)ListViewKeyPoints.SelectedItem;
            var activeTourRepository = new ActiveTourRepository();
            activeTourRepository.UpdateKeyPoint(keyPoint);
            var allKeyPoints = activeTourRepository.GetActiveTour("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void ChangeCurrentKeyPoint_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var keyPoint = (string) ListViewKeyPoints.SelectedItem;
            var activeTourRepository = new ActiveTourRepository();
            activeTourRepository.UpdateKeyPoint(keyPoint);
            var allKeyPoints = activeTourRepository.GetActiveTour("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void TouristCheckup_OnClick(object sender, RoutedEventArgs e)
        {
            var tourist = (string)ListViewTourists.SelectedItem;
            var touristRepository = new TouristRepository();
            touristRepository.CheckTouristAppearance(tourist);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void TouristCheckup_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var tourist = (string)ListViewTourists.SelectedItem;
            var touristRepository = new TouristRepository();
            touristRepository.CheckTouristAppearance(tourist);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void CheckPassedKeyPoints(IEnumerable<string> passedKeyPoints)
        {
            var counter = 0;

            foreach (var location in passedKeyPoints)
            {
                if (location.Contains("False"))
                {
                    var question = MessageBox.Show("We haven't passed all key points, are you sure you want to end the tour?", "WAIT", MessageBoxButton.YesNo);
                    if (question == MessageBoxResult.Yes)
                    {
                        counter = 0;
                        break;
                    }

                    counter = 1;
                    var currentTour = new CurrentActiveTour();
                    currentTour.Show();
                    Close();
                    break;
                }
            }

            if (counter != 0) return;
            MessageBox.Show("Tour has been finished!");
        }

        public void RemoveTour(List<string> tourDates, IReadOnlyList<string> tourists)
        {
            
            var touristRepository = new TouristRepository();
            var tourRepository = new TourRepository();
            var firstTourist = touristRepository.GetByUsername(tourists[0]);

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    var currentTourist = touristRepository.GetByUsername(tourist);
                    touristRepository.RemoveTour(currentTourist.Id);
                }
                tourRepository.Remove(firstTourist.Tour.Id);
            }
            else
            {
                var dateToday = "";
                foreach (var date in tourDates)
                {
                    if (DateTime.Compare(DateTime.Today, DateTime.Parse(date)) == 0)
                        dateToday = date;
                }

                foreach (var tourist in tourists)
                {
                    var currentTourist = touristRepository.GetByUsername(tourist);
                    touristRepository.RemoveTour(currentTourist.Id);
                }

                tourRepository.RemoveDate(dateToday, tourDates, firstTourist.Tour.Id);
            }
        }

        private void AddFinishedTour()
        {
            var finishedTourRepository = new FinishedTourRepository();
            var tourRepository = new TourRepository();
            var touristRepository = new TouristRepository();
            var activeTourRepository = new ActiveTourRepository();

            var tourists = activeTourRepository.GetActiveTour("Tourists").Split(", ");
            var firstTourist = touristRepository.GetByUsername(tourists[0]);

            var keyPoints = activeTourRepository.GetActiveTour("KeyPointsList");
            var keyPointParts = keyPoints.Split(", ");
            CheckPassedKeyPoints(keyPointParts);

            var tour = tourRepository.GetById(firstTourist.Tour.Id);

            if (finishedTourRepository.CheckExistingTours(tour))
                finishedTourRepository.Edit(new FinishedTour(tour.Id, tour.Name, tourRepository.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), touristRepository.GetByTour(tour), tour.Date.Split(", ")[0]));
            else
                finishedTourRepository.Add(new FinishedTour(tour.Id, tour.Name, tourRepository.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), touristRepository.GetByTour(tour), tour.Date.Split(", ")[0]));


            RemoveTour(tour.Date.Split(", ").ToList(), tourists);
            activeTourRepository.Remove();

        }
        private void FinishTour_OnClick(object sender, RoutedEventArgs e)
        {
            AddFinishedTour();
            var reviewTour = new ReviewTour();
            reviewTour.Show();
            Close();

        }

        private void CheckAllGuests_OnClick(object sender, RoutedEventArgs e)
        {
            var activeTourRepository = new ActiveTourRepository();
            var tourists = activeTourRepository.GetActiveTour("Tourists");
            var touristRepository = new TouristRepository();
            var counter = 0;

            foreach (var tourist in tourists.Split(", "))
            {
                if (touristRepository.GetByUsername(tourist).TouristAppearance == TouristAppearance.Unknown)
                    counter++;
            }

            if (counter == 0)
                MessageBox.Show("All tourists were already checked!");
            else
                touristRepository.CheckAllTouristAppearances(tourists);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                var guideView = new GuideView();
                guideView.Show();
                Close();
            }

            if (e.Key == Key.F2)
            {
                var monitorTour = new MonitorTour();
                monitorTour.Show();
                Close();
            }

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Show();
                Close();
            }
        }
    }
}